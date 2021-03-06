﻿using System;
using System.Xml;
using System.Xml.Linq;

using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace DXMPP
{
	namespace SASL
	{
		internal abstract class SASLMechanism
		{
			protected string DecodeBase64(string Input)
			{
				return Encoding.UTF8.GetString (System.Convert.FromBase64String (Input));
			}
			protected string EncodeBase64(string Input)
			{
				return System.Convert.ToBase64String (Encoding.UTF8.GetBytes (Input));
			}

			public enum SASLMechanisms
			{
				None = 0,
				PLAIN   = 1,
				DIGEST_MD5  = 2,
				CRAM_MD5    = 3, // Not implemented
				SCRAM_SHA1  = 4
			}

			public JID MyJID;
			public string Password;
			public X509Certificate Certificate;
			public Network.AsyncTCPXMLClient Uplink;

			public  SASLMechanism(Network.AsyncTCPXMLClient Uplink,
				JID MyJID, 
				string Password)        
			{   
				this.Uplink = Uplink;
				this.MyJID = MyJID;
				this.Password = Password;
			}
			public  SASLMechanism(Network.AsyncTCPXMLClient Uplink,
				JID MyJID, 
				X509Certificate Certificate)        
			{   
				this.Uplink = Uplink;
				this.MyJID = MyJID;
				this.Certificate = Certificate;
			}
			protected string SelectedNounce;

			public abstract void Begin ();
			public abstract void Challenge(XElement ChallengeTag);
			public abstract bool Verify(XElement SuccessTag);
		}
	}
}

