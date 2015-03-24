#region
/*
Copyright (c) 2002-2012, Bas Geertsema, Xih Solutions
(http://www.xihsolutions.net), Thiago.Sayao, Pang Wu, Ethem Evlice, Andy Phan, Chang Liu. 
All rights reserved. http://code.google.com/p/msnp-sharp/

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice,
  this list of conditions and the following disclaimer.
* Redistributions in binary form must reproduce the above copyright notice,
  this list of conditions and the following disclaimer in the documentation
  and/or other materials provided with the distribution.
* Neither the names of Bas Geertsema or Xih Solutions nor the names of its
  contributors may be used to endorse or promote products derived from this
  software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 'AS IS'
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE
LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF
THE POSSIBILITY OF SUCH DAMAGE. 
*/
#endregion

using System;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace MSNPSharp.IO
{
    using MSNPSharp.MSNWS.MSNABSharingService;
    using MSNPSharp.Core;

    /// <summary>
    /// ContactList file maintainer
    /// </summary>
    [Serializable]
    [XmlRoot("ContactList")]
    public class XMLContactList : MCLSerializer
    {
        public const string XMLContactListVersion = "20120219";

        #region Members

        [NonSerialized]
        private bool initialized = false;

        [NonSerialized]
        private int requestCircleCount = 0;

        [NonSerialized]
        private Dictionary<long, ContactType> contactTable = new Dictionary<long, ContactType>();

        [NonSerialized]
        private Dictionary<Guid, Contact> pendingAcceptionCircleList = new Dictionary<Guid, Contact>();

        [NonSerialized]
        private Dictionary<Guid, string> pendingCreateCircleList = new Dictionary<Guid, string>();

        [NonSerialized]
        private Dictionary<Guid, KeyValuePair<Contact, ContactType>> messengerContactLink = new Dictionary<Guid, KeyValuePair<Contact, ContactType>>();

        [NonSerialized]
        private Dictionary<Guid, List<ContactType>> shellContactLink = new Dictionary<Guid, List<ContactType>>();

        [NonSerialized]
        private List<ContactType> individualShellContacts = new List<ContactType>(0);

        [NonSerialized]
        private NSMessageHandler nsMessageHandler;

        private SerializableDictionary<ServiceName, ServiceMembership> mslist = new SerializableDictionary<ServiceName, ServiceMembership>(0);
        private SerializableDictionary<string, ABFindContactsPagedResultTypeAB> abInfos = new SerializableDictionary<string, ABFindContactsPagedResultTypeAB>();
        private SerializableDictionary<Guid, GroupType> groups = new SerializableDictionary<Guid, GroupType>(0);
        private SerializableDictionary<string, SerializableDictionary<Guid, ContactType>> abcontacts = new SerializableDictionary<string, SerializableDictionary<Guid, ContactType>>(0);
        private SerializableDictionary<string, CircleInverseInfoType> circleResults = new SerializableDictionary<string, CircleInverseInfoType>(0);

        #endregion

        #region Properties

        protected NSMessageHandler NSMessageHandler
        {
            get
            {
                return nsMessageHandler;
            }

            set
            {
                nsMessageHandler = value;
            }
        }

        public SerializableDictionary<ServiceName, ServiceMembership> MembershipList
        {
            get
            {
                return mslist;
            }
            set
            {
                mslist = value;
            }
        }

        public string MembershipLastChange
        {
            get
            {
                if (MembershipList.Keys.Count == 0)
                    return WebServiceConstants.ZeroTime;

                List<Service> services = new List<Service>();
                foreach (ServiceName sft in MembershipList.Keys)
                    services.Add(new Service(MembershipList[sft].Service));

                services.Sort();
                return services[services.Count - 1].LastChange;
            }
        }

        public SerializableDictionary<Guid, GroupType> Groups
        {
            get
            {
                return groups;
            }

            set
            {
                groups = value;
            }
        }

        /// <summary>
        /// The contact list for different address book pages.<br></br>
        /// The circle recreate procedure is based on this property.
        /// </summary>
        public SerializableDictionary<string, SerializableDictionary<Guid, ContactType>> AddressbookContacts
        {
            get
            {
                return abcontacts;
            }
            set
            {
                abcontacts = value;
            }
        }

        public SerializableDictionary<string, ABFindContactsPagedResultTypeAB> AddressBooksInfo
        {
            get
            {
                return abInfos;
            }

            set
            {
                abInfos = value;
            }
        }

        public SerializableDictionary<string, CircleInverseInfoType> CircleResults
        {
            get
            {
                return circleResults;
            }
            set
            {
                circleResults = value;
            }
        }

        /// <summary>
        /// Circles created by the library and waiting server's confirm.
        /// </summary>
        internal Dictionary<Guid, string> PendingCreateCircleList
        {
            get
            {
                return pendingCreateCircleList;
            }
        }

        /// <summary>
        /// A collection of all circles which are pending acception.
        /// </summary>
        private Dictionary<Guid, Contact> PendingAcceptionCircleList
        {
            get
            {
                return pendingAcceptionCircleList;
            }
        }

        /// <summary>
        /// These contacts are pure shell contacts, they don't have windows live account.
        /// Such an example is the facebook contact which imported to your buddy list but
        /// the owner doesn't have a WLM account.
        /// </summary>
        private List<ContactType> IndividualShellContacts
        {
            get
            {
                return individualShellContacts;
            }
        }

        /// <summary>
        /// These contacts are "Shells" of their corresponding messenger contact but in different network.
        /// For example, a user have both messenger and facebook account, then on his/her friend's contact list,
        /// that user's facebook contact is a shell contact of its messenger contact. The stupid M$ Windows Live team 
        /// finally realized that they cannot manage the contacts only by account and network type, they need a 
        /// foreign key instead. They should do this ten years ago and the ContactInfo data structure won't be 
        /// this ugly now.
        /// </summary>
        private Dictionary<Guid, List<ContactType>> ShellContactLink
        {
            get
            {
                return shellContactLink;
            }
        }

        /// <summary>
        /// The messenger contact which MIGHT have shell contact connected to it.
        /// </summary>
        private Dictionary<Guid, KeyValuePair<Contact, ContactType>> MessengerContactLink
        {
            get
            {
                return messengerContactLink;
            }
        }

        #endregion

        #region Public Methods

        public static XMLContactList LoadFromFile(string filename, MclSerialization st, NSMessageHandler nsHandler, bool useCache)
        {
            XMLContactList contactlist = (XMLContactList)LoadFromFile(filename, st, typeof(XMLContactList), nsHandler.Credentials.Password, useCache);
            contactlist.NSMessageHandler = nsHandler;
            return contactlist;
        }

        /// <summary>
        /// Save the <see cref="XMLContactList"/> into a specified file.
        /// </summary>
        /// <param name="filename"></param>
        public override void Save(string filename)
        {
            lock (SyncObject)
            {
                try
                {
                    Version = XMLContactListVersion;
                    base.Save(filename);
                }
                catch (Exception ex)
                {
                    Trace.WriteLineIf(Settings.TraceSwitch.TraceError,
                        "An error occurs while saving the Addressbook, StackTrace:\r\n" +
                        ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// Get a basemember from membership list.
        /// </summary>
        /// <param name="servicetype"></param>
        /// <param name="account"></param>
        /// <param name="type"></param>
        /// <param name="memberrole"></param>
        /// <returns>If the member not exist, return null.</returns>
        public BaseMember SelectBaseMember(ServiceName servicetype, string account, IMAddressInfoType type, RoleId memberrole)
        {
            SerializableDictionary<RoleId, SerializableDictionary<string, BaseMember>> ms = SelectTargetMemberships(servicetype);
            if ((ms != null) && ms.ContainsKey(memberrole))
            {
                string hash = Contact.MakeHash(account, type);
                if (ms[memberrole].ContainsKey(hash))
                    return ms[memberrole][hash];
            }
            return null;
        }

        public XMLContactList Merge(MembershipResult findMembership)
        {
            lock (SyncObject)
            {
                Initialize();

                // Process new FindMemberships (deltas)
                if (null != findMembership && null != findMembership.Services)
                {
                    foreach (ServiceType serviceType in findMembership.Services)
                    {
                        Service oldService = SelectTargetService(serviceType.Info.Handle.Type);

                        if (oldService == null ||
                            WebServiceDateTimeConverter.ConvertToDateTime(oldService.LastChange)
                            < WebServiceDateTimeConverter.ConvertToDateTime(serviceType.LastChange))
                        {
                            if (serviceType.Deleted)
                            {
                                if (MembershipList.ContainsKey(serviceType.Info.Handle.Type))
                                {
                                    MembershipList.Remove(serviceType.Info.Handle.Type);
                                }
                            }
                            else
                            {
                                Service updatedService = new Service();
                                updatedService.Id = int.Parse(serviceType.Info.Handle.Id);
                                updatedService.ServiceType = serviceType.Info.Handle.Type;
                                updatedService.LastChange = serviceType.LastChange;
                                updatedService.ForeignId = serviceType.Info.Handle.ForeignId;

                                if (oldService == null)
                                {
                                    MembershipList.Add(updatedService.ServiceType, new ServiceMembership(updatedService));
                                }

                                if (null != serviceType.Memberships)
                                {
                                    if (ServiceName.Messenger == serviceType.Info.Handle.Type ||
                                        ServiceName.IMAvailability == serviceType.Info.Handle.Type)
                                    {
                                        ProcessMessengerAndIMAvailabilityMemberships(serviceType, ref updatedService);
                                    }
                                    else
                                    {
                                        ProcessOtherMemberships(serviceType, ref updatedService);
                                    }
                                }

                                // Update service.LastChange
                                MembershipList[updatedService.ServiceType].Service = updatedService;
                            }
                        }
                    }
                }

                return this;
            }
        }

        public XMLContactList Merge(ABFindContactsPagedResultType forwardList)
        {
            Initialize();

            if (forwardList != null && forwardList.Ab != null)
            {
                if (WebServiceConstants.MessengerIndividualAddressBookId == forwardList.Ab.abId.ToLowerInvariant())
                {
                    MergeIndividualAddressBook(forwardList);
                }
                else
                {
                    MergeGroupAddressBook(forwardList);
                }
            }

            return this;
        }

        public virtual void Add(Dictionary<Service, Dictionary<RoleId, Dictionary<string, BaseMember>>> membershipRange)
        {
            lock (SyncObject)
            {
                foreach (Service svc in membershipRange.Keys)
                {
                    foreach (RoleId role in membershipRange[svc].Keys)
                    {
                        foreach (string hash in membershipRange[svc][role].Keys)
                        {
                            if (!mslist.ContainsKey(svc.ServiceType))
                                mslist.Add(svc.ServiceType, new ServiceMembership(svc));

                            if (!mslist[svc.ServiceType].Memberships.ContainsKey(role))
                                mslist[svc.ServiceType].Memberships.Add(role, new SerializableDictionary<string, BaseMember>(0));

                            if (mslist[svc.ServiceType].Memberships[role].ContainsKey(hash))
                            {
                                if (/* mslist[svc.ServiceType].Memberships[role][hash].LastChangedSpecified
                                    && */
                                    WebServiceDateTimeConverter.ConvertToDateTime(mslist[svc.ServiceType].Memberships[role][hash].LastChanged).CompareTo(
                                    WebServiceDateTimeConverter.ConvertToDateTime(membershipRange[svc][role][hash].LastChanged)) <= 0)
                                {
                                    mslist[svc.ServiceType].Memberships[role][hash] = membershipRange[svc][role][hash];
                                }
                            }
                            else
                            {
                                mslist[svc.ServiceType].Memberships[role].Add(hash, membershipRange[svc][role][hash]);
                            }
                        }
                    }
                }
            }
        }


        public virtual void Add(string abId, Dictionary<Guid, ContactType> abRange)
        {
            lock (SyncObject)
            {
                string lowerId = abId.ToLowerInvariant();

                if (!abcontacts.ContainsKey(lowerId))
                {
                    abcontacts.Add(lowerId, new SerializableDictionary<Guid, ContactType>(0));
                }

                foreach (Guid guid in abRange.Keys)
                {
                    abcontacts[lowerId][guid] = abRange[guid];
                }
            }
        }

        public void AddGroup(Dictionary<Guid, GroupType> groupRange)
        {
            foreach (GroupType group in groupRange.Values)
            {
                AddGroup(group);
            }
        }

        public void AddGroup(GroupType group)
        {
            lock (SyncObject)
            {
                Guid key = new Guid(group.groupId);
                if (groups.ContainsKey(key))
                {
                    groups[key] = group;
                }
                else
                {
                    groups.Add(key, group);
                }
            }
        }

        #endregion

        #region Initialize

        /// <summary>
        /// Initialize contacts from mcl file. Creates contacts based on MemberShipList, Groups, CircleResults and AddressbookContacts.
        /// MemberShipList, Groups, CircleResults and AddressbookContacts is pure clean and no contains DELTAS...
        /// So, member.Deleted is not valid here...
        /// </summary>
        internal void Initialize()
        {
            if (initialized)
                return;

            initialized = true;

            #region Restore Memberships

            SerializableDictionary<RoleId, SerializableDictionary<string, BaseMember>> ms =
                SelectTargetMemberships(ServiceName.Messenger);

            if (ms != null)
            {
                foreach (RoleId role in ms.Keys)
                {
                    RoleLists msnlist = ContactList.GetMSNList(role);

                    if (msnlist != RoleLists.None)
                    {
                        foreach (BaseMember bm in ms[role].Values)
                        {
                            long cid;
                            string account;
                            IMAddressInfoType type;
                            string displayname;

                            if (DetectBaseMember(bm, out account, out type, out cid, out displayname))
                            {
                                Contact contact = NSMessageHandler.ContactList.GetContact(account, displayname, type);
                                contact.Lists |= msnlist;

                                if (cid != 0)
                                    contact.CID = cid;
                            }
                        }
                    }
                }
            }

            #endregion

            #region Restore Friendship status

            SerializableDictionary<RoleId, SerializableDictionary<string, BaseMember>> fs =
               SelectTargetMemberships(ServiceName.SocialNetwork);

            if (fs != null)
            {
                foreach (RoleId role in fs.Keys)
                {
                    foreach (BaseMember bm in fs[role].Values)
                    {
                        long cid;
                        string account;
                        IMAddressInfoType type;
                        string displayname;

                        if (DetectBaseMember(bm, out account, out type, out cid, out displayname))
                        {
                            Contact contact = NSMessageHandler.ContactList.GetContact(account, displayname, type);

                            if (cid != 0)
                                contact.CID = cid;

                            contact.SetFriendshipStatus(role, false);
                        }
                    }
                }
            }

            #endregion

            #region IMAvailability

            SerializableDictionary<RoleId, SerializableDictionary<string, BaseMember>> hd =
              SelectTargetMemberships(ServiceName.IMAvailability);

            if (hd != null && hd.ContainsKey(RoleId.Hide))
            {
                foreach (BaseMember bm in hd[RoleId.Hide].Values)
                {
                    long cid;
                    string account;
                    IMAddressInfoType type;
                    string displayname;

                    if (DetectBaseMember(bm, out account, out type, out cid, out displayname))
                    {
                        Contact contact = NSMessageHandler.ContactList.GetContact(account, displayname, type);
                        contact.Lists |= RoleLists.Hide;

                        if (cid != 0)
                            contact.CID = cid;
                    }
                }
            }

            #endregion

            #region Restore Groups

            foreach (GroupType group in Groups.Values)
            {
                NSMessageHandler.ContactGroups.AddGroup(new ContactGroup(group.groupInfo.name, group.groupId, NSMessageHandler, group.groupInfo.IsFavorite));
            }

            #endregion

            #region Restore CID contact table

            foreach (string abId in AddressbookContacts.Keys)
            {
                ContactType[] contactList = new ContactType[AddressbookContacts[abId].Count];

                AddressbookContacts[abId].Values.CopyTo(contactList, 0);
                SaveContactTable(contactList);
            }

            #endregion

            #region Restore Circles

            string[] abIds = FilterWLConnections(new List<string>(CircleResults.Keys), RelationshipState.Accepted);
            RestoreCircles(abIds, RelationshipState.Accepted);
            abIds = FilterWLConnections(new List<string>(CircleResults.Keys), RelationshipState.WaitingResponse);
            RestoreCircles(abIds, RelationshipState.WaitingResponse);

            #endregion

            #region Restore default addressbook

            if (AddressbookContacts.ContainsKey(WebServiceConstants.MessengerIndividualAddressBookId))
            {
                SerializableDictionary<Guid, ContactType> defaultPage = AddressbookContacts[WebServiceConstants.MessengerIndividualAddressBookId];
                foreach (ContactType contactType in defaultPage.Values)
                {
                    Contact tmpContact;
                    ReturnState updateResult = UpdateContact(contactType, out tmpContact); //Restore contacts.
                    if ((updateResult & ReturnState.UpdateError) != ReturnState.None)
                    {
                        Trace.WriteLineIf(Settings.TraceSwitch.TraceError, "[Initialize Error]: update contact error.");
                    }
                }

                // Get all remote network contacts
                CreateContactsFromShellContact();
            }

            #endregion
        }

        #endregion

        #region Membership

        internal Service SelectTargetService(ServiceName type)
        {
            if (MembershipList.ContainsKey(type))
                return MembershipList[type].Service;

            return null;
        }

        internal SerializableDictionary<RoleId, SerializableDictionary<string, BaseMember>> SelectTargetMemberships(ServiceName serviceFilterType)
        {
            if (MembershipList.ContainsKey(serviceFilterType))
                return MembershipList[serviceFilterType].Memberships;

            return null;
        }

        /// <summary>
        /// Add a member to the underlying membership data structure.
        /// </summary>
        /// <param name="servicetype"></param>
        /// <param name="account"></param>
        /// <param name="type"></param>
        /// <param name="memberrole"></param>
        /// <param name="member"></param>
        internal void AddMemberhip(ServiceName servicetype, string account, IMAddressInfoType type, RoleId memberrole, BaseMember member)
        {
            lock (SyncObject)
            {
                SerializableDictionary<RoleId, SerializableDictionary<string, BaseMember>> ms = SelectTargetMemberships(servicetype);
                if (ms != null)
                {
                    if (!ms.ContainsKey(memberrole))
                        ms.Add(memberrole, new SerializableDictionary<string, BaseMember>(0));

                    ms[memberrole][Contact.MakeHash(account, type)] = member;
                }
            }
        }

        internal void RemoveMemberhip(ServiceName servicetype, string account, IMAddressInfoType type, RoleId memberrole)
        {
            lock (SyncObject)
            {
                SerializableDictionary<RoleId, SerializableDictionary<string, BaseMember>> ms = SelectTargetMemberships(servicetype);
                if (ms != null && ms.ContainsKey(memberrole))
                {
                    string hash = Contact.MakeHash(account, type);
                    if (ms[memberrole].ContainsKey(hash))
                    {
                        ms[memberrole].Remove(hash);
                    }
                }
            }
        }

        private bool HasMemberhip(ServiceName servicetype, string account, IMAddressInfoType type, RoleId memberrole)
        {
            SerializableDictionary<RoleId, SerializableDictionary<string, BaseMember>> ms = SelectTargetMemberships(servicetype);
            return (ms != null) && ms.ContainsKey(memberrole) && ms[memberrole].ContainsKey(Contact.MakeHash(account, type));
        }

        private void ProcessMessengerAndIMAvailabilityMemberships(ServiceType messengerService, ref Service messengerServiceClone)
        {
            #region Messenger Service memberhips

            foreach (Membership membership in messengerService.Memberships)
            {
                RoleId memberrole = membership.MemberRole;
                RoleLists msnlist = ContactList.GetMSNList(memberrole);

                if (null != membership.Members && msnlist != RoleLists.None)
                {
                    List<BaseMember> members = new List<BaseMember>(membership.Members);
                    members.Sort(CompareBaseMembers);

                    foreach (BaseMember bm in members)
                    {
                        long cid;
                        string account;
                        IMAddressInfoType type;
                        string displayname;

                        if (DetectBaseMember(bm, out account, out type, out cid, out displayname))
                        {
                            if (bm.Deleted)
                            {
                                #region Members deleted in other clients.

                                if (HasMemberhip(messengerServiceClone.ServiceType, account, type, memberrole) &&
                                    WebServiceDateTimeConverter.ConvertToDateTime(MembershipList[messengerServiceClone.ServiceType].Memberships[memberrole][Contact.MakeHash(account, type)].LastChanged)
                                    < WebServiceDateTimeConverter.ConvertToDateTime(bm.LastChanged))
                                {
                                    RemoveMemberhip(messengerServiceClone.ServiceType, account, type, memberrole);
                                }

                                if (NSMessageHandler.ContactList.HasContact(account, type))
                                {
                                    Contact contact = NSMessageHandler.ContactList.GetContact(account, type);
                                    if (cid != 0)
                                        contact.CID = cid;

                                    if (contact.HasLists(msnlist))
                                    {
                                        contact.RemoveFromList(msnlist);

                                        // Send a list remove event
                                        NSMessageHandler.ContactService.OnContactRemoved(new ListMutateEventArgs(contact, msnlist));
                                    }
                                }

                                #endregion
                            }
                            else
                            {
                                #region Newly added memberships.

                                if (false == MembershipList[messengerServiceClone.ServiceType].Memberships.ContainsKey(memberrole) ||
                                    /*new*/ false == MembershipList[messengerServiceClone.ServiceType].Memberships[memberrole].ContainsKey(Contact.MakeHash(account, type)) ||
                                    /*probably membershipid=0*/ WebServiceDateTimeConverter.ConvertToDateTime(bm.LastChanged)
                                    > WebServiceDateTimeConverter.ConvertToDateTime(MembershipList[messengerServiceClone.ServiceType].Memberships[memberrole][Contact.MakeHash(account, type)].LastChanged))
                                {
                                    AddMemberhip(messengerServiceClone.ServiceType, account, type, memberrole, bm);
                                }

                                Contact contact = NSMessageHandler.ContactList.GetContact(account, displayname, type);
                                if (cid != 0)
                                    contact.CID = cid;

                                if (!contact.HasLists(msnlist))
                                {
                                    contact.AddToList(msnlist);
                                    NSMessageHandler.ContactService.OnContactAdded(new ListMutateEventArgs(contact, msnlist));

                                    // Added by other place, this place hasn't synchronized this contact yet.
                                    if (contact.OnForwardList && contact.OnPendingList)
                                    {
                                        contact.OnPendingList = false;
                                    }
                                    // At this phase, we requested all memberships including pending.
                                    // FriendshipRequested (2/2): After SignedIn
                                    else if (contact.OnPendingList && NSMessageHandler.IsSignedIn)
                                    {
                                        NSMessageHandler.ContactService.OnFriendshipRequested(new ContactEventArgs(contact));
                                    }
                                }

                                #endregion
                            }
                        }
                    }
                }
            }

            #endregion
        }

        private void ProcessOtherMemberships(ServiceType service, ref Service serviceClone)
        {
            foreach (Membership membership in service.Memberships)
            {
                if (null != membership.Members)
                {
                    RoleId memberrole = membership.MemberRole;
                    List<BaseMember> members = new List<BaseMember>(membership.Members);
                    members.Sort(CompareBaseMembers);

                    foreach (BaseMember bm in members)
                    {
                        long cid;
                        string account;
                        IMAddressInfoType type;
                        string displayname;

                        if (DetectBaseMember(bm, out account, out type, out cid, out displayname))
                        {
                            if (bm.Deleted)
                            {
                                RemoveMemberhip(serviceClone.ServiceType, account, type, memberrole);

                                if (serviceClone.ServiceType == ServiceName.SocialNetwork)
                                {
                                    Contact contact = NSMessageHandler.ContactList.GetContactWithCreate(account, type);

                                    if (memberrole == RoleId.OneWayRelationship)
                                    {
                                        contact.SetFriendshipStatus(RoleId.None, true);
                                    }
                                    if (memberrole == RoleId.TwoWayRelationship)
                                    {
                                        contact.SetFriendshipStatus(RoleId.OneWayRelationship, true);
                                    }
                                }
                            }
                            else
                            {
                                AddMemberhip(serviceClone.ServiceType, account, type, memberrole, bm);

                                if (serviceClone.ServiceType == ServiceName.SocialNetwork)
                                {
                                    Contact contact = NSMessageHandler.ContactList.GetContactWithCreate(account, type);
                                    if (cid != 0)
                                        contact.CID = cid;

                                    contact.SetFriendshipStatus(memberrole, true);

                                    // FriendshipRequested (2/2): After SignedIn
                                    if (memberrole == RoleId.Pending && NSMessageHandler.IsSignedIn)
                                    {
                                        NSMessageHandler.ContactService.OnFriendshipRequested(new ContactEventArgs(contact));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static int CompareBaseMembers(BaseMember x, BaseMember y)
        {
            return x.LastChanged.CompareTo(y.LastChanged);
        }

        private bool DetectBaseMember(BaseMember bm,
            out string account, out IMAddressInfoType type, out long cid, out string displayname)
        {
            bool ret = false;

            account = string.Empty;
            displayname = string.Empty;
            type = IMAddressInfoType.None;
            cid = 0;

            switch (bm.Type)
            {
                case MembershipType.Passport:
                    PassportMember pm = bm as PassportMember;

                    type = IMAddressInfoType.WindowsLive;
                    cid = pm.CID;

                    if (!pm.IsPassportNameHidden)
                    {
                        account = pm.PassportName;
                    }

                    break;

                case MembershipType.Email:

                    type = IMAddressInfoType.Yahoo;
                    account = ((EmailMember)bm).Email;

                    if (bm.Annotations != null)
                    {
                        foreach (Annotation anno in bm.Annotations)
                        {
                            switch (anno.Name)
                            {
                                case AnnotationNames.MSN_IM_BuddyType:
                                    if (!String.IsNullOrEmpty(anno.Value) && anno.Value.Contains(":"))
                                    {
                                        type = (IMAddressInfoType)int.Parse(anno.Value.Split(':')[0]);
                                    }
                                    break;
                            }
                        }
                    }

                    break;

                case MembershipType.Phone:
                    type = IMAddressInfoType.Telephone;
                    account = ((PhoneMember)bm).PhoneNumber;
                    break;

                case MembershipType.ExternalID:
                    type = IMAddressInfoType.RemoteNetwork;
                    account = ((ExternalIDMember)bm).SourceID;
                    break;

                case MembershipType.Domain:
                    account = ((DomainMember)bm).DomainName;
                    break;

                case MembershipType.Circle:
                    type = IMAddressInfoType.Circle;
                    account = ((CircleMember)bm).CircleId + "@" + Contact.DefaultHostDomain;
                    break;

                case MembershipType.Role:
                case MembershipType.Service:
                case MembershipType.Everyone:
                case MembershipType.Partner:
                    type = IMAddressInfoType.None;
                    account = bm.Type + "/" + bm.MembershipId;
                    break;
            }

            if (!String.IsNullOrEmpty(account) && type != IMAddressInfoType.None)
            {
                account = account.ToLowerInvariant();
                displayname = (bm.DisplayName == null) ? account : bm.DisplayName;
                ret = true;
            }

            return ret;
        }


        #endregion

        #region Addressbook

        private bool IsContactTableEmpty()
        {
            lock (contactTable)
                return contactTable.Count == 0;
        }

        private bool HasContact(long CID)
        {
            lock (contactTable)
                return contactTable.ContainsKey(CID);
        }

        private bool HasWLConnection(string abId)
        {
            lock (CircleResults)
                return CircleResults.ContainsKey(abId);
        }

        /// <summary>
        /// Check whether we've saved the specified addressbook.
        /// </summary>
        /// <param name="abId"></param>
        /// <returns></returns>
        private bool HasAddressBook(string abId)
        {
            string lowerId = abId.ToLowerInvariant();
            if (AddressBooksInfo == null)
                return false;

            lock (AddressBooksInfo)
                return AddressBooksInfo.ContainsKey(lowerId);
        }

        /// <summary>
        /// Check whether the specific contact page exist.
        /// </summary>
        /// <param name="abId">The addressbook identifier of a specific contact page.</param>
        /// <returns></returns>
        private bool HasAddressBookContactPage(string abId)
        {
            string lowerId = abId.ToLowerInvariant();
            if (AddressbookContacts == null)
                return false;

            bool returnValue = false;

            lock (AddressbookContacts)
            {
                if (AddressbookContacts.ContainsKey(lowerId))
                {
                    if (AddressbookContacts[lowerId] != null)
                    {
                        returnValue = true;
                    }
                }
            }

            return returnValue;
        }

        internal bool HasContact(string abId, Guid contactId)
        {
            string lowerId = abId.ToLowerInvariant();
            lock (AddressbookContacts)
            {
                if (!AddressbookContacts.ContainsKey(lowerId))
                    return false;

                return AddressbookContacts[lowerId].ContainsKey(contactId);
            }
        }

        /// <summary>
        /// Get the last changed date of a specific addressbook.
        /// </summary>
        /// <param name="abId">The Guid of AddreessBook.</param>
        /// <returns></returns>
        internal string GetAddressBookLastChange(Guid abId)
        {
            return GetAddressBookLastChange(abId.ToString("D"));

        }

        /// <summary>
        /// Get the last changed date of a specific addressbook.
        /// </summary>
        /// <param name="abId">The Guid of AddreessBook.</param>
        /// <returns></returns>
        internal string GetAddressBookLastChange(string abId)
        {
            string lowerId = abId.ToLowerInvariant();

            if (HasAddressBook(lowerId))
            {
                lock (AddressBooksInfo)
                    return AddressBooksInfo[lowerId].lastChange;
            }

            return WebServiceConstants.ZeroTime;
        }

        /// <summary>
        /// Set information for a specific addressbook.
        /// </summary>
        /// <param name="abId">AddressBook guid.</param>
        /// <param name="abHeader">The addressbook info.</param>
        internal void SetAddressBookInfo(Guid abId, ABFindContactsPagedResultTypeAB abHeader)
        {
            SetAddressBookInfo(abId.ToString("D"), abHeader);
        }

        /// <summary>
        /// Set information for a specific addressbook.
        /// </summary>
        /// <param name="abId">AddressBook guid.</param>
        /// <param name="abHeader">The addressbook info.</param>
        internal void SetAddressBookInfo(string abId, ABFindContactsPagedResultTypeAB abHeader)
        {
            lock (SyncObject)
            {
                string lowerId = abId.ToLowerInvariant();
                string compareTime = GetAddressBookLastChange(lowerId);

                try
                {

                    DateTime oldTime = WebServiceDateTimeConverter.ConvertToDateTime(compareTime);
                    DateTime newTime = WebServiceDateTimeConverter.ConvertToDateTime(abHeader.lastChange);
                    if (oldTime >= newTime)
                    {
                        Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose, "Update addressbook information skipped, abId: " +
                        abId + ", LastChange: " + abHeader.lastChange + ", compared with: " + compareTime);
                        return;  //Not necessary to update.
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLineIf(Settings.TraceSwitch.TraceError, "An error occured while setting AddressBook LastChange property, abId: " +
                        abId + ", LastChange: " + abHeader.lastChange + "\r\nError message: " + ex.Message);
                    return;
                }

                SetAddressBookInfoToABInfoList(lowerId, abHeader);

                Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose, "Update addressbook information succeed, abId: " +
                        abId + ", LastChange: " + abHeader.lastChange + ", compared with: " + compareTime);
            }
        }


        /// <summary>
        /// Try to remove a contact from a specific addressbook.
        /// </summary>
        /// <param name="abId">The specific addressbook identifier.</param>
        /// <param name="contactId">The contact identifier.</param>
        /// <returns>If the contact exists and removed successfully, return true, else return false.</returns>
        internal bool RemoveContactFromAddressBook(Guid abId, Guid contactId)
        {
            return RemoveContactFromAddressBook(abId.ToString("D"), contactId);
        }

        /// <summary>
        /// Try to remove a contact from a specific addressbook.
        /// </summary>
        /// <param name="abId">The specific addressbook identifier.</param>
        /// <param name="contactId">The contact identifier.</param>
        /// <returns>If the contact exists and removed successfully, return true, else return false.</returns>
        internal bool RemoveContactFromAddressBook(string abId, Guid contactId)
        {
            lock (SyncObject)
            {
                string lowerId = abId.ToLowerInvariant();
                lock (AddressbookContacts)
                {
                    if (AddressbookContacts.ContainsKey(lowerId))
                    {
                        if (AddressbookContacts[lowerId].ContainsKey(contactId))
                        {
                            return AddressbookContacts[lowerId].Remove(contactId);
                        }
                    }
                }

                return false;
            }
        }


        private bool RemoveContactFromContactTable(long CID)
        {
            lock (contactTable)
                return contactTable.Remove(CID);
        }

        /// <summary>
        /// Remove an item in AddressBooksInfo property by giving an addressbook Id.
        /// </summary>
        /// <param name="abId"></param>
        /// <returns></returns>
        private bool RemoveAddressBookInfo(string abId)
        {

            lock (AddressBooksInfo)
                return AddressBooksInfo.Remove(abId.ToLowerInvariant());
        }

        /// <summary>
        /// Remove an item from AddressbookContacts property.
        /// </summary>
        /// <param name="abId">The addressbook page of a specified contact page.</param>
        /// <returns></returns>
        private bool RemoveAddressBookContatPage(string abId)
        {
            lock (AddressbookContacts)
            {
                return AddressbookContacts.Remove(abId.ToLowerInvariant());
            }
        }

        /// <summary>
        /// Add or update a contact in the specific address book.
        /// </summary>
        /// <param name="abId">The identifier of addressbook.</param>
        /// <param name="contact">The contact to be added/updated.</param>
        /// <returns>If the contact added to the addressbook, returen true, if the contact is updated (not add), return false.</returns>
        internal bool SetContactToAddressBookContactPage(string abId, ContactType contact)
        {
            lock (SyncObject)
            {
                string lowerId = abId.ToLowerInvariant();
                bool returnval = false;

                lock (AddressbookContacts)
                {
                    if (!AddressbookContacts.ContainsKey(lowerId))
                    {
                        AddressbookContacts.Add(lowerId, new SerializableDictionary<Guid, ContactType>());
                        returnval = true;
                    }

                    AddressbookContacts[lowerId][new Guid(contact.contactId)] = contact;
                }

                return returnval;
            }
        }

        private bool SetAddressBookInfoToABInfoList(string abId, ABFindContactsPagedResultTypeAB abInfo)
        {
            string lowerId = abId.ToLowerInvariant();
            if (AddressBooksInfo == null)
                return false;

            lock (AddressBooksInfo)
                AddressBooksInfo[lowerId] = abInfo;
            return true;
        }

        private bool IsPendingCreateConfirmCircle(string abId)
        {
            lock (PendingCreateCircleList)
                return PendingCreateCircleList.ContainsKey(new Guid(abId));
        }

        private bool IsPendingCreateConfirmCircle(Guid abId)
        {
            lock (PendingCreateCircleList)
                return PendingCreateCircleList.ContainsKey(abId);
        }

        /// <summary>
        /// Get a contact from a specific addressbook by providing the addressbook identifier and contact identifier.
        /// </summary>
        /// <param name="abId">The addressbook identifier.</param>
        /// <param name="contactId">The contactidentifier.</param>
        /// <returns>If the contact exist, return the contact object, else return null.</returns>
        internal ContactType SelectContactFromAddressBook(string abId, Guid contactId)
        {
            string lowerId = abId.ToLowerInvariant();
            if (!HasContact(abId, contactId))
                return null;
            return AddressbookContacts[lowerId][contactId];
        }

        /// <summary>
        /// Get the hidden representative's CID by providing addressbook Id from the inverse connection list.
        /// </summary>
        /// <param name="abId"></param>
        /// <returns></returns>
        private CircleInverseInfoType SelectWLConnection(string abId)
        {
            if (string.IsNullOrEmpty(abId))
                return null;

            string lowerId = abId.ToLowerInvariant();
            if (!HasWLConnection(lowerId))
                return null;

            lock (CircleResults)
                return CircleResults[lowerId];
        }

        private string[] SelectWLConnection(List<string> abIds, RelationshipState state)
        {
            List<string> results = new List<string>(0);

            lock (CircleResults)
            {
                foreach (string abId in abIds)
                {
                    if (HasWLConnection(abId))
                    {
                        if (state == RelationshipState.None)
                        {
                            results.Add(abId);
                        }
                        else
                        {
                            if (CircleResults[abId.ToLowerInvariant()].PersonalInfo.MembershipInfo.CirclePersonalMembership.State == state.ToString())
                                results.Add(abId);
                        }

                    }
                }
            }

            return results.ToArray();
        }

        private string[] FilterWLConnections(List<string> abIds, RelationshipState state)
        {
            List<string> returnValues = new List<string>(0);

            foreach (string abId in abIds)
            {
                string lowerId = abId.ToLowerInvariant();
                if (CircleResults.ContainsKey(lowerId))
                {
                    CircleInverseInfoType inverseInfo = CircleResults[lowerId];
                    if (inverseInfo.PersonalInfo.MembershipInfo.CirclePersonalMembership.State == state.ToString())
                        returnValues.Add(abId);
                }
            }

            return returnValues.ToArray();
        }

        private CircleInverseInfoType SelectCircleInverseInfo(string abId)
        {
            if (string.IsNullOrEmpty(abId))
                return null;

            abId = abId.ToLowerInvariant();

            lock (CircleResults)
            {
                if (!CircleResults.ContainsKey(abId))
                    return null;
                return CircleResults[abId];
            }
        }

        /// <summary>
        /// Get a hidden representative for a addressbook by CID.
        /// </summary>
        /// <param name="CID"></param>
        /// <returns></returns>
        private ContactType SelecteContact(long CID)
        {
            if (!HasContact(CID))
            {
                return null;
            }

            lock (contactTable)
                return contactTable[CID];
        }

        /// <summary>
        /// Update members for circles.
        /// </summary>
        /// <param name="forwardList"></param>
        private void MergeGroupAddressBook(ABFindContactsPagedResultType forwardList)
        {
            lock (SyncObject)
            {
                #region Get Individual AddressBook Information (Circle information)

                if (forwardList.Ab != null && forwardList.Ab.abId != WebServiceConstants.MessengerIndividualAddressBookId &&
                    forwardList.Ab.abInfo.AddressBookType == AddressBookType.Group &&
                    WebServiceDateTimeConverter.ConvertToDateTime(GetAddressBookLastChange(forwardList.Ab.abId)) <
                    WebServiceDateTimeConverter.ConvertToDateTime(forwardList.Ab.lastChange))
                {
                    SetAddressBookInfo(forwardList.Ab.abId, forwardList.Ab);
                    SaveAddressBookContactPage(forwardList.Ab.abId, forwardList.Contacts);

                    //Create or update circle.
                    Contact targetCircle = UpdateCircleFromAddressBook(forwardList.Ab.abId);

                    if (targetCircle != null)
                    {
                        //Update circle mebers.
                        UpdateCircleMembersFromAddressBookContactPage(targetCircle, Scenario.Initial);
                        switch (targetCircle.CircleRole)
                        {
                            case RoleId.Admin:
                            case RoleId.AssistantAdmin:
                            case RoleId.Member:
                                AddCircleToCircleList(targetCircle);
                                break;

                            case RoleId.StatePendingOutbound:
                                FireJoinCircleInvitationReceivedEvents(targetCircle);

                                break;
                        }

                        if (IsPendingCreateConfirmCircle(targetCircle.AddressBookId))
                        {
                            FireCreateCircleCompletedEvent(targetCircle);
                        }

                        #region Print Info

                        Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose, "Getting non-default addressbook: \r\nId: " +
                                    forwardList.Ab.abId + "\r\nName: " + forwardList.Ab.abInfo.name +
                                    "\r\nType: " + forwardList.Ab.abInfo.AddressBookType + "\r\nMembers:");

                        foreach (ContactType contact in forwardList.Contacts)
                        {
                            Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose, "PassportName: " + contact.contactInfo.passportName + ", DisplayName: " + contact.contactInfo.displayName + ", Type: " + contact.contactInfo.contactType);
                        }

                        Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose, "\r\n");

                        #endregion

                        SaveContactTable(forwardList.Contacts);

                        if (requestCircleCount > 0)
                        {
                            //Only the individual addressbook merge which contains new circles will cause this action.
                            requestCircleCount--;
                            if (requestCircleCount == 0)
                            {
                                Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose,
                                    "************This is the initial circle ADL, should be sent after the initial contact ADL **********"
                                );

                                Save();
                                NSMessageHandler.SendInitialADL(Scenario.SendInitialCirclesADL);
                            }
                        }
                    }
                    else
                    {
                        RemoveCircleInverseInfo(forwardList.Ab.abId);
                        RemoveAddressBookContatPage(forwardList.Ab.abId);
                        RemoveAddressBookInfo(forwardList.Ab.abId);

                        //Error? Save!
                        Save();

                        Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose,
                            "An error occured while merging the GroupAddressBook, addressbook info removed: " +
                            forwardList.Ab.abId);
                    }
                }

                #endregion
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="circle"></param>
        /// <returns></returns>
        /// <remarks>This function must be called after the ContactTable and WLConnections are created.</remarks>
        private bool FireJoinCircleInvitationReceivedEvents(Contact circle)
        {
            lock (PendingAcceptionCircleList)
            {
                PendingAcceptionCircleList[circle.AddressBookId] = circle;
            }

            CircleEventArgs joinArgs = new CircleEventArgs(circle);
            NSMessageHandler.ContactService.OnJoinCircleInvitationReceived(joinArgs);
            return true;
        }

        private bool FireCreateCircleCompletedEvent(Contact newCircle)
        {
            Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose, "Newly created circle detected, create circle operation succeeded: " + newCircle);
            RemoveABIdFromPendingCreateCircleList(newCircle.AddressBookId);
            NSMessageHandler.ContactService.OnCreateCircleCompleted(new CircleEventArgs(newCircle));
            return true;
        }

        private ContactType SelectMeContactFromAddressBookContactPage(string abId)
        {
            if (!HasAddressBookContactPage(abId))
                return null;
            lock (AddressbookContacts)
                return SelectMeContactFromContactList((new List<ContactType>(AddressbookContacts[abId.ToLowerInvariant()].Values)).ToArray());
        }

        /// <summary>
        /// Get the addressbook's owner contact.
        /// </summary>
        /// <param name="contactList"></param>
        /// <returns></returns>
        private ContactType SelectMeContactFromContactList(ContactType[] contactList)
        {
            if (contactList == null)
                return null;

            foreach (ContactType contact in contactList)
            {
                if (contact.contactInfo != null)
                {
                    if (contact.contactInfo.contactType == MessengerContactType.Me)
                        return contact;
                }
            }

            return null;
        }

        internal Guid SelectSelfContactGuid(string abId)
        {
            ContactType self = SelectSelfContactFromAddressBookContactPage(abId);
            if (self == null)
                return Guid.Empty;

            return new Guid(self.contactId);
        }

        private ContactType SelectSelfContactFromAddressBookContactPage(string abId)
        {
            if (!HasAddressBookContactPage(abId))
                return null;

            if (NSMessageHandler.Owner == null)
                return null;

            lock (AddressbookContacts)
                return SelectSelfContactFromContactList((new List<ContactType>(AddressbookContacts[abId.ToLowerInvariant()].Values)).ToArray(), NSMessageHandler.Owner.Account);
        }

        /// <summary>
        /// Get the owner of default addressbook in a certain addressbook page. This contact will used for exiting circle.
        /// </summary>
        /// <param name="contactList"></param>
        /// <param name="currentUserAccount"></param>
        /// <returns></returns>
        private ContactType SelectSelfContactFromContactList(ContactType[] contactList, string currentUserAccount)
        {
            if (contactList == null)
                return null;

            string lowerAccount = currentUserAccount.ToLowerInvariant();

            foreach (ContactType contact in contactList)
            {
                if (contact.contactInfo != null)
                {
                    if (contact.contactInfo.passportName.ToLowerInvariant() == lowerAccount)
                        return contact;
                }
            }

            return null;
        }

        /// <summary>
        /// Update the circle members and other information from a newly receive addressbook.
        /// This function can only be called after the contact page and WL connections were saved.
        /// </summary>
        /// <param name="abId"></param>
        /// <returns></returns>
        private Contact UpdateCircleFromAddressBook(string abId)
        {
            if (abId != WebServiceConstants.MessengerIndividualAddressBookId)
            {
                string lowerId = abId.ToLowerInvariant();

                ContactType meContact = SelectMeContactFromAddressBookContactPage(lowerId);
                CircleInverseInfoType inverseInfo = SelectCircleInverseInfo(lowerId);
                return CreateCircle(meContact, inverseInfo);
            }

            return null;
        }

        private bool UpdateCircleMembersFromAddressBookContactPage(Contact circle, Scenario scene)
        {
            string lowerId = circle.AddressBookId.ToString("D").ToLowerInvariant();
            if (!HasAddressBookContactPage(lowerId))
                return false;

            Dictionary<long, ContactType> newContactList = null;
            Dictionary<long, Contact> oldContactInverseList = null;
            Contact[] oldContactList = null;

            bool isRestore = ((scene & Scenario.Restore) != Scenario.None);

            if (!isRestore)
            {
                newContactList = new Dictionary<long, ContactType>();
                oldContactInverseList = new Dictionary<long, Contact>();
                oldContactList = circle.ContactList.ToArray(IMAddressInfoType.Circle);
                foreach (Contact contact in oldContactList)
                {
                    oldContactInverseList[contact.CID] = contact;
                }
            }

            lock (AddressbookContacts)
            {
                SerializableDictionary<Guid, ContactType> page = AddressbookContacts[lowerId];

                foreach (ContactType contactType in page.Values)
                {
                    if (!isRestore)
                        newContactList[contactType.contactInfo.CID] = contactType;

                    Contact tmpContact;
                    if ((UpdateContact(contactType, lowerId, circle, out tmpContact) & ReturnState.ProcessNextContact) == ReturnState.None)
                    {
                        Trace.WriteLineIf(Settings.TraceSwitch.TraceInfo, "[UpdateCircleMembersFromAddressBookContactPage] Create circle member failed: " +
                            contactType.contactInfo.passportName + ", UpdateContact returns false.");
                    }
                }
            }

            if (isRestore)
                return true;

            foreach (ContactType contactType in newContactList.Values)
            {
                if (contactType.contactInfo == null)
                    continue;

                string passportName = contactType.contactInfo.passportName;

                if (String.IsNullOrEmpty(passportName) && contactType.contactInfo.emails != null)
                {
                    foreach (contactEmailType emailType in contactType.contactInfo.emails)
                    {
                        if (emailType.contactEmailType1 == ContactEmailTypeType.ContactEmailMessenger &&
                            !String.IsNullOrEmpty(emailType.email))
                        {
                            passportName = emailType.email;
                            break;
                        }
                    }
                }

                if (!oldContactInverseList.ContainsKey(contactType.contactInfo.CID) &&
                    circle.ContactList.HasContact(passportName, IMAddressInfoType.WindowsLive))
                {
                    circle.NSMessageHandler.ContactService.OnCircleMemberJoined(new CircleMemberEventArgs(circle,
                        circle.ContactList.GetContactWithCreate(passportName, IMAddressInfoType.WindowsLive)));
                }
            }

            foreach (Contact contact in oldContactList)
            {
                if (!newContactList.ContainsKey(contact.CID))
                {
                    Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose, "Member " + contact.ToString() + " has left circle " + circle.ToString());
                    circle.ContactList.Remove(contact.Account, contact.ClientType);
                    circle.NSMessageHandler.ContactService.OnCircleMemberLeft(new CircleMemberEventArgs(circle, contact));
                }
            }

            return true;
        }

        private void MergeIndividualAddressBook(ABFindContactsPagedResultType forwardList)
        {
            lock (SyncObject)
            {
                #region Get Default AddressBook Information

                if (forwardList.Ab != null && forwardList.Ab.abId == WebServiceConstants.MessengerIndividualAddressBookId &&
                        WebServiceDateTimeConverter.ConvertToDateTime(GetAddressBookLastChange(forwardList.Ab.abId)) <
                        WebServiceDateTimeConverter.ConvertToDateTime(forwardList.Ab.lastChange))
                {
                    Scenario scene = Scenario.None;

                    if (IsContactTableEmpty())
                        scene = Scenario.Initial;
                    else
                        scene = Scenario.DeltaRequest;

                    #region Get groups

                    if (null != forwardList.Groups)
                    {
                        foreach (GroupType groupType in forwardList.Groups)
                        {
                            Guid key = new Guid(groupType.groupId);
                            if (groupType.fDeleted)
                            {
                                Groups.Remove(key);

                                ContactGroup contactGroup = NSMessageHandler.ContactGroups[groupType.groupId];
                                if (contactGroup != null)
                                {
                                    NSMessageHandler.ContactGroups.RemoveGroup(contactGroup);
                                    NSMessageHandler.ContactService.OnContactGroupRemoved(new ContactGroupEventArgs(contactGroup));
                                }
                            }
                            else
                            {
                                Groups[key] = groupType;

                                // Add a new group                                    
                                NSMessageHandler.ContactGroups.AddGroup(
                                    new ContactGroup(System.Web.HttpUtility.UrlDecode(groupType.groupInfo.name), groupType.groupId, NSMessageHandler, groupType.groupInfo.IsFavorite));

                                // Fire the event
                                NSMessageHandler.ContactService.OnContactGroupAdded(
                                    new ContactGroupEventArgs(NSMessageHandler.ContactGroups[groupType.groupId]));
                            }
                        }
                    }

                    #endregion

                    #region Process Contacts

                    Dictionary<string, CircleInverseInfoType> newInverseInfos = new Dictionary<string, CircleInverseInfoType>();

                    Dictionary<string, CircleInverseInfoType> modifiedConnections = new Dictionary<string, CircleInverseInfoType>();

                    if (forwardList.CircleResult != null && forwardList.CircleResult.Circles != null)
                    {
                        foreach (CircleInverseInfoType info in forwardList.CircleResult.Circles)
                        {
                            string abId = info.Content.Handle.Id.ToLowerInvariant();

                            if (HasWLConnection(abId))
                            {
                                if (!modifiedConnections.ContainsKey(abId))
                                {
                                    modifiedConnections[abId] = info;
                                }
                            }
                            else
                            {
                                newInverseInfos[abId] = info;
                            }
                        }
                    }

                    if (null != forwardList.Contacts)
                    {
                        #region Update the messenger contacts.
                        foreach (ContactType contactType in forwardList.Contacts)
                        {
                            if (null != contactType.contactInfo)
                            {
                                SetContactToAddressBookContactPage(forwardList.Ab.abId, contactType);

                                /*
                                 * Circle update rules:
                                 * 1. If your own circle has any update (i.e. adding or deleting members), no hidden representative will be changed, only circle inverse info will be provided.
                                 * 2. If a remote owner removes you from his circle, the hidden representative of that circle will change its RelationshipState to 2, circle inverse info will not provided.
                                 * 3. If a remote owner re-adds you into a circle which you've left before, the hidden representative will be created, its relationshipState is 3, and the circle inverse info will be provided.
                                 * 4. If you are already in a circle, the circle's owner removed you, then add you back, the hidden representative's RelationshipState property in NetworkInfo will change from 2 to 3.
                                 * 5. If a remote contact has left your own circle, hidden representative will not change but circle inverse info will be provided.
                                 * 6. If you delete your own circle, the hidden representative's contactType will change, and circle reverse info will be provided.
                                 * 7. If you create a circle, the hidden representative will also create and circle inverse info will be provided.
                                 * 8. If a remote owner invites you to join a circle, the hidden representative will be created, its relationshipState is 1 and circle inverse info will be provided, Role = StatePendingOutbound.
                                 */
                                long CID = contactType.contactInfo.CID;

                                if (HasContact(CID))
                                {
                                    //modifiedConnections[CID] = SelectCircleInverseInfo(SelectWLConnection(CID));

                                    ContactType savedContact = SelecteContact(contactType.contactInfo.CID);
                                    //A deleted or modified circle; We are NOT in initial scene.

                                    if (savedContact.contactInfo.contactType == MessengerContactType.Circle)
                                    {
                                        if (savedContact.contactInfo.contactType != contactType.contactInfo.contactType)
                                        {
                                            //Owner deleted circles found.
                                            //The members in the circle which this contact represents are all livepending contacts.
                                            //Or, the circle this contact represents has no member.
                                            //ModifyCircles(contactType, forwardList.CircleResult);

                                            Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose, "A deleted circle found: contactType: " + contactType.contactInfo.contactType + "\r\n " +
                                                "CID: " + contactType.contactInfo.CID.ToString() + "\r\n " +
                                                "PassportName: " + contactType.contactInfo.passportName + "\r\n " +
                                                "DomainTag: " + GetHiddenRepresentativeDomainTag(contactType) + "\r\n " +
                                                "RelationshipState: " + GetCircleMemberRelationshipStateFromNetworkInfo(contactType.contactInfo.NetworkInfoList).ToString()
                                                + "\r\n");
                                        }
                                        else
                                        {
                                            //We may remove by the circle owner, so a circle is deleted.
                                            //ModifyCircles(contactType, forwardList.CircleResult);

                                            Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose, "A modified circle found: contactType: " + contactType.contactInfo.contactType + "\r\n " +
                                                "CID: " + contactType.contactInfo.CID.ToString() + "\r\n " +
                                                "PassportName: " + contactType.contactInfo.passportName + "\r\n " +
                                                "DomainTag: " + GetHiddenRepresentativeDomainTag(contactType) + "\r\n " +
                                                "RelationshipState: " + GetCircleMemberRelationshipStateFromNetworkInfo(contactType.contactInfo.NetworkInfoList).ToString()
                                                + "\r\n");
                                        }

                                        continue;
                                    }
                                }
                                else
                                {

                                    if (contactType.contactInfo.contactType == MessengerContactType.Circle)
                                    {

                                        //We get the hidden representative of a new circle.
                                        Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose, "A circle contact found: contactType: " + contactType.contactInfo.contactType + "\r\n " +
                                            "CID: " + contactType.contactInfo.CID.ToString() + "\r\n " +
                                            "PassportName: " + contactType.contactInfo.passportName + "\r\n " +
                                            "DomainTag: " + GetHiddenRepresentativeDomainTag(contactType) + "\r\n " +
                                            "RelationshipState: " + GetCircleMemberRelationshipStateFromNetworkInfo(contactType.contactInfo.NetworkInfoList).ToString()
                                            + "\r\n");
                                        continue;
                                    }
                                }

                                Contact contact = NSMessageHandler.ContactList.GetContactByGuid(new Guid(contactType.contactId));

                                if (contactType.fDeleted)
                                {
                                    //The contact was deleted.

                                    RemoveContactFromAddressBook(forwardList.Ab.abId, new Guid(contactType.contactId));

                                    if (contact != null)
                                    {
                                        contact.RemoveFromList(RoleLists.Forward);
                                        NSMessageHandler.ContactService.OnContactRemoved(new ListMutateEventArgs(contact, RoleLists.Forward));

                                        contact.Guid = Guid.Empty;
                                        contact.SetIsMessengerUser(false);

                                        PresenceStatus oldStatus = contact.Status;
                                        PresenceStatus newStatus = PresenceStatus.Offline;
                                        contact.SetStatus(newStatus);  //Force the contact offline.
                                        NSMessageHandler.OnContactStatusChanged(new ContactStatusChangedEventArgs(contact, oldStatus, newStatus));
                                        NSMessageHandler.OnContactOffline(new ContactStatusChangedEventArgs(contact, oldStatus, newStatus));

                                        if (RoleLists.None == contact.Lists)
                                        {
                                            NSMessageHandler.ContactList.Remove(contact.Account, contact.ClientType);
                                            contact.NSMessageHandler = null;
                                        }
                                    }
                                }
                                else
                                {
                                    if (UpdateContact(contactType, out contact) != ReturnState.UpdateError &&
                                        contact != null)
                                    {
                                        NSMessageHandler.ContactService.OnContactAdded(new ListMutateEventArgs(contact, RoleLists.Forward));
                                    }
                                }
                            }
                        }
                        #endregion


                        #region
                        CreateContactsFromShellContact();

                        #endregion

                    }

                    if (forwardList.Ab != null)
                    {
                        // Update lastchange
                        SetAddressBookInfo(forwardList.Ab.abId, forwardList.Ab);
                    }

                    SaveContactTable(forwardList.Contacts);
                    if (forwardList.CircleResult != null)
                        SaveCircleInverseInfo(forwardList.CircleResult.Circles);


                    ProcessCircles(modifiedConnections, newInverseInfos, scene);
                }

                    #endregion

                #endregion
            }
        }

        private void ProcessCircles(Dictionary<string, CircleInverseInfoType> modifiedConnections, Dictionary<string, CircleInverseInfoType> newInverseInfos, Scenario scene)
        {
            // We must process modified circles first.
            ProcessModifiedCircles(modifiedConnections, scene | Scenario.ModifiedCircles);
            ProcessNewConnections(newInverseInfos, scene | Scenario.NewCircles);
        }

        private int[] ProcessNewConnections(Dictionary<string, CircleInverseInfoType> newInverseInfos, Scenario scene)
        {
            int added = 0;
            int pending = 0;

            SaveWLConnection(newInverseInfos);
            List<string> abIds = new List<string>(newInverseInfos.Keys);

            string[] filteredAbIds = SelectWLConnection(abIds, RelationshipState.Accepted);
            RequestCircles(filteredAbIds, RelationshipState.Accepted, scene);
            added = filteredAbIds.Length;

            filteredAbIds = SelectWLConnection(abIds, RelationshipState.WaitingResponse);
            RequestCircles(filteredAbIds, RelationshipState.WaitingResponse, scene);
            pending = filteredAbIds.Length;

            return new int[] { added, pending };
        }

        private int[] ProcessModifiedCircles(Dictionary<string, CircleInverseInfoType> modifiedConnections, Scenario scene)
        {
            int deleted = 0;
            int reAdded = 0;

            Dictionary<string, CircleInverseInfoType> connectionClone = new Dictionary<string, CircleInverseInfoType>(modifiedConnections);
            foreach (string abId in modifiedConnections.Keys)
            {
                CircleInverseInfoType inverseInfo = SelectWLConnection(abId);
                if (inverseInfo != null && modifiedConnections[abId].Deleted)
                {
                    RemoveCircle(modifiedConnections[abId].Content.Handle.Id, true);
                    connectionClone.Remove(abId);
                    deleted++;
                }
            }

            SaveWLConnection(connectionClone);

            string[] slectedABIds = SelectWLConnection(new List<string>(connectionClone.Keys), RelationshipState.Accepted);  //Select the re-added circles.
            RequestCircles(slectedABIds, RelationshipState.Accepted, scene);
            reAdded = slectedABIds.Length;

            return new int[] { deleted, reAdded };
        }

        private bool SaveWLConnection(Dictionary<string, CircleInverseInfoType> inverseList)
        {
            if (inverseList == null)
                return false;

            lock (CircleResults)
            {
                foreach (string abId in inverseList.Keys)
                {
                    CircleResults[abId] = inverseList[abId];
                }
            }

            return true;
        }

        private bool SaveAddressBookContactPage(string abId, ContactType[] contacts)
        {
            if (contacts == null)
                return false;

            lock (AddressbookContacts)
            {
                SerializableDictionary<Guid, ContactType> page = new SerializableDictionary<Guid, ContactType>(0);
                AddressbookContacts[abId.ToLowerInvariant()] = page;
                foreach (ContactType contact in contacts)
                {
                    page[new Guid(contact.contactId)] = contact;
                }
            }

            return true;
        }

        private void SaveCircleInverseInfo(CircleInverseInfoType[] inverseInfoList)
        {
            if (inverseInfoList != null)
            {
                foreach (CircleInverseInfoType circle in inverseInfoList)
                {
                    string lowerId = circle.Content.Handle.Id.ToLowerInvariant();

                    lock (CircleResults)
                    {

                        CircleResults[lowerId] = circle;
                    }
                }
            }

        }

        private void SaveContactTable(ContactType[] contacts)
        {
            if (contacts == null)
                return;

            lock (contactTable)
            {
                foreach (ContactType contact in contacts)
                {
                    if (contact.contactInfo != null)
                    {
                        contactTable[contact.contactInfo.CID] = contact;
                    }
                }
            }
        }

        /// <summary>
        /// Clean up the saved circle addressbook information.
        /// </summary>
        internal void ClearCircleInfos()
        {
            lock (SyncObject)
            {
                //lock (CircleResults)
                CircleResults.Clear();

                //lock (AddressBooksInfo)
                AddressBooksInfo.Clear();

                //lock (AddressbookContacts)
                AddressbookContacts.Clear();

                //lock (contactTable)
                contactTable.Clear();
            }
        }

        /// <summary>
        /// 1. RemoveAddressBookContatPage
        /// 2. RemoveAddressBookInfo
        /// 3. RemoveCircleInverseInfo
        /// 4. BreakWLConnection
        /// 5. RemoveCircle
        /// </summary>
        /// <param name="abId"></param>
        /// <param name="breakConnection"></param>
        /// <returns></returns>
        internal bool RemoveCircle(string abId, bool breakConnection)
        {
            lock (SyncObject)
            {
                if (!string.IsNullOrEmpty(abId))
                {
                    CircleInverseInfoType inversoeInfo = SelectCircleInverseInfo(abId);
                    Contact tempCircle = null;
                    if (inversoeInfo != null)
                    {
                        string circleMail = abId + "@" + inversoeInfo.Content.Info.HostedDomain;
                        tempCircle = NSMessageHandler.ContactList.GetCircle(circleMail);
                    }

                    //1. Remove corresponding addressbook page.
                    RemoveAddressBookContatPage(abId);

                    //2. Remove addressbook info.
                    RemoveAddressBookInfo(abId);

                    //3. Remove circle inverse info.
                    RemoveCircleInverseInfo(abId);

                    if (breakConnection)
                    {
                        //4. Break the connection between hidden representative and addressbook.
                        BreakWLConnection(abId);

                        //5. Remove the presentation data structure for a circle.
                        string circleMail2 = abId + "@" + Contact.DefaultHostDomain;
                        NSMessageHandler.ContactList.Remove(circleMail2, IMAddressInfoType.Circle);

                        if (tempCircle != null)
                        {
                            NSMessageHandler.ContactService.OnExitCircleCompleted(new CircleEventArgs(tempCircle));
                        }
                    }

                    return true;
                }

                return false;
            }
        }

        internal bool RemoveCircleInverseInfo(string abId)
        {
            lock (SyncObject)
                return CircleResults.Remove(abId.ToLowerInvariant());
        }

        /// <summary>
        /// Break the CID-AbID relationship of hidden representative to addressbook.
        /// </summary>
        /// <param name="abId"></param>
        /// <returns></returns>
        private bool BreakWLConnection(string abId)
        {
            if (!HasWLConnection(abId))
                return false;

            if (!HasWLConnection(abId))
                return false;

            lock (CircleResults)
            {
                return CircleResults.Remove(abId);
            }
        }

        private bool RemoveABIdFromPendingCreateCircleList(Guid abId)
        {
            lock (PendingCreateCircleList)
                return PendingCreateCircleList.Remove(abId);
        }

        /// <summary>
        /// Get a circle addressbook by addressbook identifier.
        /// </summary>
        /// <param name="abId"></param>
        /// <param name="state"></param>
        /// <param name="scene"></param>
        /// <returns></returns>
        private bool RequestAddressBookByABId(string abId, RelationshipState state, Scenario scene)
        {
            Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose, "Requesting AddressBook by ABId: " + abId + ", Scenario: " + scene.ToString());

            abId = abId.ToLowerInvariant();

            abHandleType individualAB = new abHandleType();
            individualAB.ABId = abId;
            individualAB.Cid = 0;
            individualAB.Puid = 0;

            switch (state)
            {
                case RelationshipState.Accepted:
                    requestCircleCount++;
                    try
                    {
                        NSMessageHandler.ContactService.abRequest(PartnerScenario.Initial, individualAB, null);
                    }
                    catch (Exception ex1)
                    {
                        requestCircleCount--;
                        Trace.WriteLineIf(Settings.TraceSwitch.TraceError, "[RequestAddressBookByABId] Error: " + ex1.Message);
                    }
                    break;
                case RelationshipState.WaitingResponse:
                    try
                    {
                        NSMessageHandler.ContactService.abRequest(PartnerScenario.Initial, individualAB, null);
                    }
                    catch (Exception ex2)
                    {
                        Trace.WriteLineIf(Settings.TraceSwitch.TraceError, "[RequestAddressBookByABId] Error: " + ex2.Message);
                    }
                    break;
            }

            return true;
        }

        /// <summary>
        /// Create a circle.
        /// </summary>
        /// <param name="me"></param>
        /// <param name="inverseInfo"></param>
        /// <returns></returns>
        private Contact CreateCircle(ContactType me, CircleInverseInfoType inverseInfo)
        {
            if (me == null || me.contactInfo == null)
            {
                Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose, "[CreateCircle] Cannot create circle since Me not found in addressbook.");
                return null;
            }

            if (inverseInfo == null || inverseInfo.PersonalInfo == null ||
                inverseInfo.Content == null || inverseInfo.Content.Info == null || inverseInfo.Content.Handle == null)
            {
                Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose, "[CreateCircle] Cannot create circle since inverse info not found in circle result list.");
                return null;
            }

            string circleMail = inverseInfo.Content.Handle.Id.ToLowerInvariant() + "@" + inverseInfo.Content.Info.HostedDomain.ToLowerInvariant();
            Contact circle = NSMessageHandler.ContactList.GetContactWithCreate(circleMail, IMAddressInfoType.Circle);

            circle.SetCircleInfo(inverseInfo, me);

            return circle;
        }

        private bool AddCircleToCircleList(Contact circle)
        {
            NSMessageHandler.CircleList[circle.Hash] = circle;

            lock (PendingAcceptionCircleList)
            {
                if (PendingAcceptionCircleList.ContainsKey(circle.AddressBookId))
                {
                    NSMessageHandler.ContactService.OnJoinedCircleCompleted(new CircleEventArgs(circle));
                }

                PendingAcceptionCircleList.Remove(circle.AddressBookId);
            }

            return true;
        }

        private bool RestoreCircles(string[] abIds, RelationshipState state)
        {
            if (abIds == null)
                return false;

            foreach (string abId in abIds)
            {
                RestoreCircleFromAddressBook(abId, state);
            }

            return true;
        }

        private bool RestoreCircleFromAddressBook(string abId, RelationshipState state)
        {
            string lowerId = abId.ToLowerInvariant();

            if (lowerId == WebServiceConstants.MessengerIndividualAddressBookId)
                return true;

            if (!HasAddressBook(lowerId))
            {
                Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose, "[RestoreCircleFromAddressBook] failed, cannot find specific addressbook :" + lowerId);
                return false;
            }

            if (!AddressbookContacts.ContainsKey(lowerId))
            {
                Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose, "[RestoreCircleFromAddressBook] failed, cannot find specific addressbook contact group:" + lowerId);
                return false;
            }

            //We use addressbook list to boot the restore procedure.
            ContactType me = SelectMeContactFromContactList(new List<ContactType>(AddressbookContacts[lowerId].Values).ToArray());
            CircleInverseInfoType inverseInfo = SelectCircleInverseInfo(lowerId);

            if (me == null)
            {
                Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose, "[RestoreCircleFromAddressBook] Me Contact not found, restore circle failed:" + lowerId);
                return false;
            }

            if (inverseInfo == null)
            {
                Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose, "[RestoreCircleFromAddressBook] Circle inverse info not found, restore circle failed:" + lowerId);
                return false;
            }

            string circleMail = lowerId + "@" + Contact.DefaultHostDomain;

            Contact circle = NSMessageHandler.ContactList.GetCircle(circleMail);

            if (circle != null && circle.circleInfo != null)
            {
                Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose, "[RestoreCircleFromAddressBook] circle inverse info already exists, restore skipped:" + lowerId);
                return false;
            }

            circle = CreateCircle(me, inverseInfo);

            if (circle != null)
            {
                UpdateCircleMembersFromAddressBookContactPage(circle, Scenario.Restore);

                switch (circle.CircleRole)
                {
                    case RoleId.Admin:
                    case RoleId.AssistantAdmin:
                    case RoleId.Member:
                        AddCircleToCircleList(circle);
                        break;

                    case RoleId.StatePendingOutbound:
                        FireJoinCircleInvitationReceivedEvents(circle);
                        break;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Use msn webservices to get addressbooks.
        /// </summary>
        /// <param name="abIds"></param>
        /// <param name="state"></param>
        /// <param name="scene"></param>
        private void RequestCircles(string[] abIds, RelationshipState state, Scenario scene)
        {
            if (abIds == null)
                return;

            foreach (string abId in abIds)
            {
                RequestAddressBookByABId(abId, state, scene);
            }
        }

        private Contact CreateGatewayContact(NetworkInfoType networkInfo)
        {
            if (!String.IsNullOrEmpty(networkInfo.DomainTag) &&
                    networkInfo.DomainTag != WebServiceConstants.NullDomainTag &&
                    networkInfo.SourceId == SourceId.FaceBook &&
                    networkInfo.DomainId == DomainIds.FaceBookDomain)
            {
                // Here we create the gateway from Me contact.
                Contact gatewayContact = NSMessageHandler.ContactList.GetContactWithCreate(RemoteNetworkGateways.FaceBookGatewayAccount, IMAddressInfoType.RemoteNetwork);
                gatewayContact.Lists |= RoleLists.Forward;

                Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose, "Gateway " + gatewayContact + " added to network contacts", GetType().Name);

                return gatewayContact;
            }

            Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose,
                "[Warning] Unknown Getway found, please implement this gateway:\r\n" +
                "DomainTag: " + networkInfo.DomainTag + "\r\n" +
                "DomainId: " + networkInfo.DomainId + "\r\n" +
                "SourceId: " + networkInfo.SourceId + "\r\n");

            return null;
        }

        private void CreateContactsFromShellContact()
        {
            lock (MessengerContactLink)
            {
                foreach (Guid persionID in MessengerContactLink.Keys)
                {
                    CreateContactFromLinkedShellContact(persionID, SourceId.FaceBook);
                    CreateContactFromLinkedShellContact(persionID, SourceId.LinkedIn);
                    CreateContactFromLinkedShellContact(persionID, SourceId.MySpace);
                }
            }

            lock (IndividualShellContacts)
            {
                foreach (ContactType individualShellContact in IndividualShellContacts)
                {
                    CreateContactFromIndividualShellContact(individualShellContact);
                }
            }
        }

        private void CreateContactsFromLinkedShellContact(ContactType contactType)
        {
            if (contactType.contactInfo.LinkInfo != null)
            {
                Guid persionID = new Guid(contactType.contactInfo.LinkInfo.PersonID);

                CreateContactFromLinkedShellContact(persionID, SourceId.FaceBook);
                CreateContactFromLinkedShellContact(persionID, SourceId.LinkedIn);
                CreateContactFromLinkedShellContact(persionID, SourceId.MySpace);
            }
        }

        private Contact CreateContactFromIndividualShellContact(ContactType individualShellContact)
        {
            if (individualShellContact.contactInfo == null)
            {
                Trace.WriteLineIf(Settings.TraceSwitch.TraceError,
                            "Create Contact from ShellContact error. Try to create shell contact from individualShellContact, but contactInfo is null.");
                return null;
            }

            if (individualShellContact.contactInfo.SourceHandle == null)
            {
                Trace.WriteLineIf(Settings.TraceSwitch.TraceError,
                            "Create Contact from ShellContact error. Try to create shell contact from individualShellContact, but individualShellContact.contactInfo.SourceHandle is null.");
                return null;
            }

            switch (individualShellContact.contactInfo.SourceHandle.SourceID)
            {
                case SourceId.FaceBook:
                    Contact facebookContact = CreateFaceBookContactFromShellContact(individualShellContact);
                    Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose, facebookContact + ":" + facebookContact.Name + " added to connect contacts", GetType().Name);

                    return facebookContact;
                case SourceId.LinkedIn:
                    break;
                case SourceId.MySpace:
                    break;
            }

            return null;
        }

        /// <summary>
        /// Use to create a facebook contact from an individual <see cref="ShellContact"/>.
        /// </summary>
        /// <param name="individualShellContact"></param>
        /// <returns></returns>
        private Contact CreateFaceBookContactFromShellContact(ContactType individualShellContact)
        {
            return CreateFaceBookContactFromShellContact(null, individualShellContact, true);
        }

        private Contact CreateFaceBookContactFromShellContact(Contact coreContact, ContactType shellContact, bool isIndividualShellContact)
        {
            Contact facebookGatewayContact = NSMessageHandler.ContactList.GetContactWithCreate(
                                            RemoteNetworkGateways.FaceBookGatewayAccount,
                                            IMAddressInfoType.RemoteNetwork);
            Contact facebookContact = null;

            if (shellContact.contactInfo.NetworkInfoList != null)
            {
                foreach (NetworkInfoType networkInfo in shellContact.contactInfo.NetworkInfoList)
                {
                    if (!String.IsNullOrEmpty(networkInfo.DomainTag) &&
                        networkInfo.DomainTag != WebServiceConstants.NullDomainTag)
                    {
                        if (networkInfo.SourceId == SourceId.FaceBook && networkInfo.DomainId == DomainIds.FaceBookDomain)
                        {
                            facebookContact = facebookGatewayContact.ContactList.CreateShellContact(
                                coreContact, IMAddressInfoType.Connect, networkInfo.DomainTag);

                            if (!String.IsNullOrEmpty(networkInfo.UserTileURL))
                            {
                                facebookContact.UserTileURL = new Uri(networkInfo.UserTileURL);
                            }
                            return facebookContact;
                        }
                    }
                }
            }
            else
            {
                facebookContact = facebookGatewayContact.ContactList.CreateShellContact(coreContact, IMAddressInfoType.Connect, shellContact.contactInfo.SourceHandle.ObjectID);

                if (shellContact.contactInfo.URLs != null)
                {
                    foreach (ContactURLType contactURL in shellContact.contactInfo.URLs)
                    {
                        if (String.IsNullOrEmpty(contactURL.URL) == false &&
                            contactURL.URLType == URLType.Other && contactURL.URLName == URLName.UserTileXL)
                        {
                            facebookContact.UserTileURL = new Uri(contactURL.URL);
                        }
                    }
                }
            }

            if (isIndividualShellContact)
            {
                facebookContact.SetName(shellContact.contactInfo.firstName + " " + shellContact.contactInfo.lastName);
            }

            return facebookContact;
        }

        /// <summary>
        /// Convert all shell contacts to their corresponding contacts in different networks. 
        /// </summary>
        /// <param name="personID">
        /// A <see cref="Guid"/>
        /// </param>
        /// <param name="sourceID">
        /// The sring of network gateway.
        /// </param>
        /// <returns>
        /// A <see cref="Contact"/>
        /// </returns>
        private Contact CreateContactFromLinkedShellContact(Guid personID, string sourceID)
        {
            if (!MessengerContactLink.ContainsKey(personID))
            {
                return null;
            }

            Contact messengerContact = MessengerContactLink[personID].Key;

            if (messengerContact == null)
            {
                Trace.WriteLineIf(Settings.TraceSwitch.TraceError, "Create Contact from ShellContact error, Messenger Contact is null.");
                return null;
            }

            lock (ShellContactLink)
            {
                #region Create From NetworkInfoList

                if (!ShellContactLink.ContainsKey(personID))
                {
                    //That means this contact links to itself, all info is in NetworkInfoList. What a crappy design!
                    ContactType contactType = MessengerContactLink[personID].Value;
                    if (contactType.contactInfo == null)
                    {
                        Trace.WriteLineIf(Settings.TraceSwitch.TraceError,
                            "Create Contact from ShellContact error. Try to create shell contact from NetworkInfoList, but contactInfo is null.");
                        return null;
                    }

                    if (contactType.contactInfo.NetworkInfoList == null)
                    {
                        Trace.WriteLineIf(Settings.TraceSwitch.TraceError,
                            "Create Contact from ShellContact error. Try to create shell contact from NetworkInfoList, but NetworkInfoList is null.");
                        return null;
                    }

                    foreach (NetworkInfoType networkInfo in contactType.contactInfo.NetworkInfoList)
                    {
                        if (!String.IsNullOrEmpty(networkInfo.DomainTag) &&
                            networkInfo.DomainTag != WebServiceConstants.NullDomainTag &&
                            networkInfo.SourceId == sourceID)
                        {
                            switch (sourceID)
                            {
                                case SourceId.FaceBook:
                                    if (networkInfo.DomainId == DomainIds.FaceBookDomain)
                                    {
                                        Contact facebookContact = CreateFaceBookContactFromShellContact(messengerContact, contactType, false);
                                        Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose, facebookContact + ":" + facebookContact.Name + " added to connect contacts", GetType().Name);

                                        return facebookContact;
                                    }
                                    break;
                                case SourceId.LinkedIn:
                                    // LinkedIn contacts.
                                    break;
                                case SourceId.MySpace:
                                    // MySpace contacts.
                                    break;
                            }
                        }
                    }

                    return null;
                }

                #endregion

                List<ContactType> shellContacts = ShellContactLink[personID];
                foreach (ContactType shellContact in shellContacts)
                {
                    switch (sourceID)
                    {
                        case SourceId.FaceBook:
                            // Facebook contacts.
                            if (shellContact.contactInfo.SourceHandle.SourceID == sourceID)
                            {
                                Contact facebookContact = CreateFaceBookContactFromShellContact(messengerContact, shellContact, false);
                                Trace.WriteLineIf(Settings.TraceSwitch.TraceVerbose, facebookContact + ":" + facebookContact.Name + " added to connect contacts", GetType().Name);

                                return facebookContact;
                            }
                            break;
                        case SourceId.LinkedIn:
                            // LinkedIn contacts.
                            break;
                        case SourceId.MySpace:
                            // MySpace contacts.
                            break;
                    }
                }
            }

            return null;
        }


        private ReturnState UpdateContact(ContactType contactType, out Contact updatedContact)
        {
            return UpdateContact(contactType, WebServiceConstants.MessengerIndividualAddressBookId, null, out updatedContact);
        }

        private ReturnState UpdateContact(ContactType contactType, string abId, Contact circle, out Contact updatedContact)
        {
            if (contactType.contactInfo == null)
            {
                Trace.WriteLineIf(Settings.TraceSwitch.TraceError, "Cannot update contact, contact info is null.");
                updatedContact = null;
                return ReturnState.UpdateError;
            }

            contactInfoType contactInfo = contactType.contactInfo;
            IMAddressInfoType type = IMAddressInfoType.WindowsLive;
            string account = contactInfo.passportName;
            string displayName = contactInfo.displayName;
            string nickName = GetContactNickName(contactType);
            Uri userTileURL = GetUserTileURLFromWindowsLiveNetworkInfo(contactType);
            bool isMessengerUser = contactInfo.isMessengerUser;
            string lowerId = abId.ToLowerInvariant();
            ReturnState returnValue = ReturnState.ProcessNextContact;
            ContactList contactList = null;
            bool isDefaultAddressBook = (lowerId == null || lowerId == WebServiceConstants.MessengerIndividualAddressBookId);
            bool isHidden = contactType.contactInfo.IsHiddenSpecified ? contactType.contactInfo.IsHidden : false;
            bool isShellContact = contactType.contactInfo.IsShellContactSpecified ? contactType.contactInfo.IsShellContact : false;


            if (isShellContact)
            {
                if (contactInfo.LinkInfo != null)
                {
                    Guid persionID = new Guid(contactInfo.LinkInfo.PersonID);
                    lock (ShellContactLink)
                    {
                        if (!ShellContactLink.ContainsKey(persionID))
                        {
                            ShellContactLink[persionID] = new List<ContactType>(0);
                        }

                        ShellContactLink[persionID].Add(contactType);
                    }
                    updatedContact = null;
                    return ReturnState.ProcessNextContact;
                }
                else
                {
                    lock (IndividualShellContacts)
                    {
                        IndividualShellContacts.Add(contactType);
                        updatedContact = null;
                        return ReturnState.ProcessNextContact;
                    }
                }
            }

            if (contactInfo.emails != null && account == null && contactInfo != null)
            {
                foreach (contactEmailType cet in contactInfo.emails)
                {
                    if (cet.isMessengerEnabled)
                    {
                        type = (IMAddressInfoType)Enum.Parse(typeof(IMAddressInfoType), cet.Capability);
                        account = cet.email;
                        isMessengerUser |= cet.isMessengerEnabled;
                        displayName = account;
                        break;
                    }
                }
            }

            if (contactInfo.phones != null && account == null)
            {
                foreach (contactPhoneType cpt in contactInfo.phones)
                {
                    if (cpt.isMessengerEnabled)
                    {
                        type = IMAddressInfoType.Telephone;
                        account = cpt.number;
                        isMessengerUser |= cpt.isMessengerEnabled;
                        displayName = account;
                        break;
                    }
                }
            }

            if (account != null)
            {
                account = account.ToLowerInvariant();
                if (contactInfo.contactType != MessengerContactType.Me)
                {
                    #region Contacts other than owner

                    Contact contact = null;

                    if (isDefaultAddressBook)
                    {
                        contact = NSMessageHandler.ContactList.GetContactWithCreate(account, type);
                        contactList = NSMessageHandler.ContactList;
                        if (contact == null)
                        {
                            updatedContact = null;
                            return ReturnState.UpdateError;
                        }

                        if (contactInfo.LinkInfo != null && isMessengerUser)
                        {
                            lock (MessengerContactLink)
                            {
                                // There might be shell contact connect with this contact.
                                // Add it to link info.
                                Guid persionID = new Guid(contactInfo.LinkInfo.PersonID);
                                MessengerContactLink[persionID] = new KeyValuePair<Contact, ContactType>(contact, contactType);
                            }
                        }
                    }
                    else
                    {
                        if (circle == null)
                        {
                            Trace.WriteLineIf(Settings.TraceSwitch.TraceError, "Cannot update contact: " + account + " in addressbook: " + abId);

                            //This means we are restoring contacts from mcl file.
                            //We need to retore the circle first, then initialize this contact again.
                            updatedContact = null;
                            return ReturnState.UpdateError;
                        }

                        RoleId membershipRole = GetCircleMemberRoleFromNetworkInfo(contactInfo.NetworkInfoList);
                        contact = circle.ContactList.GetContactWithCreate(account, type);
                        contactList = circle.ContactList;
                        contact.CircleRole = membershipRole;
                        string tempName = GetCircleMemberDisplayNameFromNetworkInfo(contactInfo.NetworkInfoList);
                        if (!string.IsNullOrEmpty(tempName))
                            displayName = tempName;
                    }

                    contact.Guid = new Guid(contactType.contactId);
                    contact.CID = Convert.ToInt64(contactInfo.CID);
                    contact.ContactType = contactInfo.contactType;
                    contact.SetHasSpace(contactInfo.hasSpace);
                    contact.SetComment(contactInfo.comment);
                    contact.SetIsMessengerUser(isMessengerUser);
                    contact.SetMobileAccess(contactInfo.isMobileIMEnabled);
                    contact.UserTileURL = userTileURL;
                    if (contactInfo.firstName != "")
                        displayName = contactInfo.firstName + " " + contactInfo.lastName;

                    SetContactPhones(contact, contactInfo);

                    if (!string.IsNullOrEmpty(nickName) && string.IsNullOrEmpty(contact.NickName))
                    {
                        contact.SetNickName(nickName);
                    }


                    if (contact.IsMessengerUser)
                    {
                        contact.AddToList(RoleLists.Forward); //IsMessengerUser is only valid in AddressBook member
                    }

                    if (!string.IsNullOrEmpty(displayName))
                    {
                        if ((contact.Name == contact.Account && displayName != contact.Account) ||
                            string.IsNullOrEmpty(contact.Name))
                        {
                            contact.SetName(displayName);
                        }
                    }


                    if (contactInfo.groupIds != null)
                    {
                        foreach (string groupId in contactInfo.groupIds)
                        {
                            contact.ContactGroups.Add(NSMessageHandler.ContactGroups[groupId]);
                        }
                    }

                    if (contactInfo.groupIdsDeleted != null)
                    {
                        foreach (string groupId in contactInfo.groupIdsDeleted)
                        {
                            contact.ContactGroups.Remove(NSMessageHandler.ContactGroups[groupId]);
                        }
                    }

                    #endregion

                    #region Filter yourself and members who alrealy left this circle.
                    bool needsDelete = false;

                    RelationshipState relationshipState = GetCircleMemberRelationshipStateFromNetworkInfo(contactInfo.NetworkInfoList);
                    if (((relationshipState & RelationshipState.Rejected) != RelationshipState.None ||
                        relationshipState == RelationshipState.None) &&
                        isDefaultAddressBook == false)
                    {
                        //Members who already left.
                        needsDelete |= true;
                    }

                    if (isHidden && !isShellContact)
                    {
                        needsDelete |= true;
                    }

                    if (account == NSMessageHandler.Owner.Account.ToLowerInvariant() &&
                        contactInfo.NetworkInfoList != null &&
                        type == NSMessageHandler.Owner.ClientType &&
                        isDefaultAddressBook == false)
                    {
                        //This is a self contact. If we need to left a circle, we need its contactId.
                        //The exit circle operation just delete this contact from addressbook.
                        needsDelete |= true;
                    }

                    if (contactType.fDeleted)
                    {
                        needsDelete |= true;
                    }

                    if (needsDelete && contact.Lists == RoleLists.None && contact.FriendshipStatus == RoleId.None)
                    {
                        contactList.Remove(account, type);
                    }

                    #endregion

                    updatedContact = contact;
                }
                else
                {
                    #region Update owner and Me contact

                    Owner owner = null;

                    if (lowerId == WebServiceConstants.MessengerIndividualAddressBookId)
                    {
                        owner = NSMessageHandler.Owner;
                        if (owner == null)
                        {
                            owner = new Owner(abId, contactInfo.passportName, Convert.ToInt64(contactInfo.CID), NSMessageHandler);
                            NSMessageHandler.ContactList.SetOwner(owner);
                        }
                    }
                    else
                    {
                        if (circle == null)
                        {
                            Trace.WriteLineIf(Settings.TraceSwitch.TraceError, "Cannot update owner: " + account + " in addressbook: " + abId);
                            updatedContact = null;
                            return ReturnState.UpdateError;
                        }

                        owner = circle.ContactList.Owner;
                    }

                    if (displayName == owner.Account && !String.IsNullOrEmpty(owner.Name))
                    {
                        displayName = owner.Name;
                    }

                    owner.Guid = new Guid(contactType.contactId);
                    owner.CID = Convert.ToInt64(contactInfo.CID);
                    owner.ContactType = contactInfo.contactType;

                    if (!string.IsNullOrEmpty(displayName) && string.IsNullOrEmpty(owner.Name))
                    {
                        //We set display name by the addressbook information only if it's initially empty.
                        owner.SetName(displayName);
                    }

                    if (!string.IsNullOrEmpty(nickName) && string.IsNullOrEmpty(owner.NickName))
                    {
                        owner.SetNickName(nickName);
                    }

                    owner.UserTileURL = userTileURL;
                    SetContactPhones(owner, contactInfo);

                    #endregion

                    // Networks...
                    if (contactInfo.NetworkInfoList != null)
                    {
                        foreach (NetworkInfoType networkInfo in contactInfo.NetworkInfoList)
                        {
                            if (!String.IsNullOrEmpty(networkInfo.DomainTag) &&
                                networkInfo.DomainTag != WebServiceConstants.NullDomainTag &&
                                networkInfo.SourceId == SourceId.FaceBook &&
                                networkInfo.DomainId == DomainIds.FaceBookDomain)
                            {
                                CreateGatewayContact(networkInfo);
                            }
                        }
                    }


                    updatedContact = owner;
                }
            }
            else
            {
                updatedContact = null;
            }

            return returnValue;
        }

        private bool SetContactPhones(Contact contact, contactInfoType cinfo)
        {
            if (cinfo == null || cinfo.phones == null)
                return false;

            foreach (contactPhoneType cp in cinfo.phones)
            {
                contact.PhoneNumbers[cp.contactPhoneType1] = cp.number;
            }

            return true;
        }

        /// <summary>
        /// Get a contact's nick name from it's Annotations.
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        private string GetContactNickName(ContactType contact)
        {
            if (contact.contactInfo == null)
                return string.Empty;

            if (contact.contactInfo.annotations == null)
                return string.Empty;

            foreach (Annotation anno in contact.contactInfo.annotations)
            {
                if (anno.Name == AnnotationNames.AB_NickName)
                {
                    return anno.Value;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Get windows live user title url.
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        private Uri GetUserTileURLFromWindowsLiveNetworkInfo(ContactType contact)
        {
            string returnURL = GetUserTileURLByDomainIdFromNetworkInfo(contact, DomainIds.WindowsLiveDomain);
            try
            {
                Uri urlResult = null;
                if (Uri.TryCreate(returnURL, UriKind.Absolute, out urlResult))
                    return urlResult;
            }
            catch (Exception)
            {

            }

            return null;
        }

        /// <summary>
        /// Get user title url.
        /// </summary>
        /// <param name="contact"></param>
        /// <param name="domainId"></param>
        /// <returns></returns>
        private string GetUserTileURLByDomainIdFromNetworkInfo(ContactType contact, int domainId)
        {
            if (contact.contactInfo == null || contact.contactInfo.NetworkInfoList == null)
                return string.Empty;

            foreach (NetworkInfoType info in contact.contactInfo.NetworkInfoList)
            {
                if (info.DomainId != 0 && info.DomainId == domainId)
                {
                    if (!string.IsNullOrEmpty(info.UserTileURL))
                    {
                        return info.UserTileURL;
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Get a contact's RelationshipState property by providing DomainId = 1 and RelationshipType = 5.
        /// </summary>
        /// <param name="infoList"></param>
        /// <returns></returns>
        private RelationshipState GetCircleMemberRelationshipStateFromNetworkInfo(NetworkInfoType[] infoList)
        {
            return (RelationshipState)GetContactRelationshipStateFromNetworkInfo(infoList, DomainIds.WindowsLiveDomain, RelationshipTypes.CircleGroup);
        }

        /// <summary>
        /// Get a contact's RelationshipState property by providing DomainId and RelationshipType
        /// </summary>
        /// <param name="infoList"></param>
        /// <param name="domainId"></param>
        /// <param name="relationshipType"></param>
        /// <returns></returns>
        private int GetContactRelationshipStateFromNetworkInfo(NetworkInfoType[] infoList, int domainId, int relationshipType)
        {
            if (infoList == null)
                return 0;

            foreach (NetworkInfoType info in infoList)
            {
                if (info.RelationshipType != 0 && info.DomainId != 0 && info.RelationshipState != 0)
                {
                    if (info.DomainId == domainId && info.RelationshipType == relationshipType)
                    {
                        return info.RelationshipState;
                    }
                }
            }

            return 0;
        }

        private string GetCircleMemberDisplayNameFromNetworkInfo(NetworkInfoType[] infoList)
        {
            return GetContactDisplayNameFromNetworkInfo(infoList, DomainIds.WindowsLiveDomain, RelationshipTypes.CircleGroup);
        }

        private string GetContactDisplayNameFromNetworkInfo(NetworkInfoType[] infoList, int domainId, int relationshipType)
        {
            if (infoList == null)
                return string.Empty;

            foreach (NetworkInfoType info in infoList)
            {
                if (info.RelationshipType != 0 && info.DomainId != 0 && !string.IsNullOrEmpty(info.DisplayName))
                {
                    if (info.DomainId == domainId && info.RelationshipType == relationshipType)
                    {
                        return info.DisplayName;
                    }
                }
            }

            return string.Empty;
        }


        private RoleId GetCircleMemberRoleFromNetworkInfo(NetworkInfoType[] infoList)
        {
            return (RoleId)GetContactRelationshipRoleFromNetworkInfo(infoList, DomainIds.WindowsLiveDomain, RelationshipTypes.CircleGroup);
        }

        private long GetContactRelationshipRoleFromNetworkInfo(NetworkInfoType[] infoList, int domainId, int relationshipType)
        {
            if (infoList == null)
                return 0;

            foreach (NetworkInfoType info in infoList)
            {
                if (info.RelationshipType != 0 && info.DomainId != 0 && info.RelationshipRole != 0)
                {
                    if (info.DomainId == domainId && info.RelationshipType == relationshipType)
                    {
                        return info.RelationshipRole;
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Get the domain tage of circle's hidden repersentative.
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        private string GetHiddenRepresentativeDomainTag(ContactType contact)
        {
            if (contact.contactInfo == null)
                return string.Empty;

            if (contact.contactInfo.contactType != MessengerContactType.Circle)
                return string.Empty;

            return GetDomainTagFromNetworkInfo(contact.contactInfo.NetworkInfoList, DomainIds.WindowsLiveDomain);
        }

        private string GetDomainTagFromNetworkInfo(NetworkInfoType[] infoList, int domainId)
        {
            if (infoList == null)
                return string.Empty;

            foreach (NetworkInfoType info in infoList)
            {
                if (info.DomainId != 0 && !string.IsNullOrEmpty(info.DomainTag))
                {
                    if (info.DomainId == domainId)
                        return info.DomainTag;
                }
            }

            return string.Empty;
        }

        private bool AddToContactTable(long CID, ContactType contact)
        {
            if (contact.contactInfo == null)
                return false;

            lock (contactTable)
                contactTable[CID] = contact;
            return true;
        }

        #endregion
    }
};
