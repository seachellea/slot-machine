﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SlotMachine
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Grp1-Casino")]
	public partial class CasinoDBDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public CasinoDBDataContext() : 
				base(global::SlotMachine.Properties.Settings.Default.Grp1_CasinoConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public CasinoDBDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CasinoDBDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CasinoDBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CasinoDBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.uspCheckActive")]
		public ISingleResult<uspCheckActiveResult> uspCheckActive([global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserID", DbType="NChar(13)")] string userID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userID);
			return ((ISingleResult<uspCheckActiveResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.uspGetActiveMachines")]
		public ISingleResult<uspGetActiveMachinesResult> uspGetActiveMachines()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<uspGetActiveMachinesResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.uspGetAllHistory")]
		public ISingleResult<uspGetAllHistoryResult> uspGetAllHistory()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<uspGetAllHistoryResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.uspGetCurrentLoss")]
		public ISingleResult<uspGetCurrentLossResult> uspGetCurrentLoss([global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserID", DbType="NChar(13)")] string userID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userID);
			return ((ISingleResult<uspGetCurrentLossResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.uspGetUserName")]
		public ISingleResult<uspGetUserNameResult> uspGetUserName([global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserID", DbType="NChar(13)")] string userID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userID);
			return ((ISingleResult<uspGetUserNameResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.uspLatestUser")]
		public ISingleResult<uspLatestUserResult> uspLatestUser()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<uspLatestUserResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.uspLogin")]
		public ISingleResult<uspLoginResult> uspLogin([global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserID", DbType="NChar(13)")] string userID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userID);
			return ((ISingleResult<uspLoginResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.uspNewUser")]
		public int uspNewUser([global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserID", DbType="NChar(13)")] string userID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="LastName", DbType="NVarChar(50)")] string lastName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="FirstName", DbType="NVarChar(50)")] string firstName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Birthday", DbType="Date")] System.Nullable<System.DateTime> birthday, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Address", DbType="NVarChar(200)")] string address, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Email", DbType="NVarChar(100)")] string email, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="ContactNumber", DbType="NChar(11)")] string contactNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="MemberSince", DbType="Date")] System.Nullable<System.DateTime> memberSince, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Status", DbType="Bit")] System.Nullable<bool> status)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userID, lastName, firstName, birthday, address, email, contactNumber, memberSince, status);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.uspRetrieveBalance")]
		public ISingleResult<uspRetrieveBalanceResult> uspRetrieveBalance([global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserID", DbType="NChar(13)")] string userID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userID);
			return ((ISingleResult<uspRetrieveBalanceResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.uspTransact")]
		public int uspTransact([global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserID", DbType="NChar(13)")] string userID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="MachineID", DbType="Int")] System.Nullable<int> machineID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="GameID", DbType="Int")] System.Nullable<int> gameID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Spent", DbType="Int")] System.Nullable<int> spent, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Winnings", DbType="Int")] System.Nullable<int> winnings, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Result", DbType="Bit")] System.Nullable<bool> result, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Details", DbType="NVarChar(MAX)")] string details)
		{
			IExecuteResult result1 = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userID, machineID, gameID, spent, winnings, result, details);
			return ((int)(result1.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.uspUpdateCurrentLoss")]
		public int uspUpdateCurrentLoss([global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserID", DbType="NChar(13)")] string userID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Change", DbType="Bit")] System.Nullable<bool> change)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userID, change);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.uspUpdateMachineStatus")]
		public int uspUpdateMachineStatus([global::System.Data.Linq.Mapping.ParameterAttribute(Name="MachineID", DbType="Int")] System.Nullable<int> machineID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Status", DbType="Bit")] System.Nullable<bool> status, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserID", DbType="NChar(13)")] string userID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="GameID", DbType="Int")] System.Nullable<int> gameID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machineID, status, userID, gameID);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.uspCheckActive2")]
		public ISingleResult<uspCheckActive2Result> uspCheckActive2([global::System.Data.Linq.Mapping.ParameterAttribute(Name="MachineID", DbType="Int")] System.Nullable<int> machineID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machineID);
			return ((ISingleResult<uspCheckActive2Result>)(result.ReturnValue));
		}
	}
	
	public partial class uspCheckActiveResult
	{
		
		private string _ActivePlayer;
		
		public uspCheckActiveResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ActivePlayer", DbType="NChar(13)")]
		public string ActivePlayer
		{
			get
			{
				return this._ActivePlayer;
			}
			set
			{
				if ((this._ActivePlayer != value))
				{
					this._ActivePlayer = value;
				}
			}
		}
	}
	
	public partial class uspGetActiveMachinesResult
	{
		
		private int _MachineID;
		
		private string _ActivePlayer;
		
		private string _UserName;
		
		private System.Nullable<int> _ActiveGame;
		
		private string _GameName;
		
		public uspGetActiveMachinesResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MachineID", DbType="Int NOT NULL")]
		public int MachineID
		{
			get
			{
				return this._MachineID;
			}
			set
			{
				if ((this._MachineID != value))
				{
					this._MachineID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ActivePlayer", DbType="NChar(13)")]
		public string ActivePlayer
		{
			get
			{
				return this._ActivePlayer;
			}
			set
			{
				if ((this._ActivePlayer != value))
				{
					this._ActivePlayer = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserName", DbType="NVarChar(101) NOT NULL", CanBeNull=false)]
		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				if ((this._UserName != value))
				{
					this._UserName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ActiveGame", DbType="Int")]
		public System.Nullable<int> ActiveGame
		{
			get
			{
				return this._ActiveGame;
			}
			set
			{
				if ((this._ActiveGame != value))
				{
					this._ActiveGame = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GameName", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string GameName
		{
			get
			{
				return this._GameName;
			}
			set
			{
				if ((this._GameName != value))
				{
					this._GameName = value;
				}
			}
		}
	}
	
	public partial class uspGetAllHistoryResult
	{
		
		private int _TransactionID;
		
		private string _UserID;
		
		private int _MachineID;
		
		private int _GameID;
		
		private int _Spent;
		
		private int _Winnings;
		
		private System.DateTime _TransactionDate;
		
		public uspGetAllHistoryResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TransactionID", DbType="Int NOT NULL")]
		public int TransactionID
		{
			get
			{
				return this._TransactionID;
			}
			set
			{
				if ((this._TransactionID != value))
				{
					this._TransactionID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", DbType="NChar(13) NOT NULL", CanBeNull=false)]
		public string UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				if ((this._UserID != value))
				{
					this._UserID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MachineID", DbType="Int NOT NULL")]
		public int MachineID
		{
			get
			{
				return this._MachineID;
			}
			set
			{
				if ((this._MachineID != value))
				{
					this._MachineID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GameID", DbType="Int NOT NULL")]
		public int GameID
		{
			get
			{
				return this._GameID;
			}
			set
			{
				if ((this._GameID != value))
				{
					this._GameID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Spent", DbType="Int NOT NULL")]
		public int Spent
		{
			get
			{
				return this._Spent;
			}
			set
			{
				if ((this._Spent != value))
				{
					this._Spent = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Winnings", DbType="Int NOT NULL")]
		public int Winnings
		{
			get
			{
				return this._Winnings;
			}
			set
			{
				if ((this._Winnings != value))
				{
					this._Winnings = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TransactionDate", DbType="DateTime NOT NULL")]
		public System.DateTime TransactionDate
		{
			get
			{
				return this._TransactionDate;
			}
			set
			{
				if ((this._TransactionDate != value))
				{
					this._TransactionDate = value;
				}
			}
		}
	}
	
	public partial class uspGetCurrentLossResult
	{
		
		private int _CurrentLoss;
		
		public uspGetCurrentLossResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CurrentLoss", DbType="Int NOT NULL")]
		public int CurrentLoss
		{
			get
			{
				return this._CurrentLoss;
			}
			set
			{
				if ((this._CurrentLoss != value))
				{
					this._CurrentLoss = value;
				}
			}
		}
	}
	
	public partial class uspGetUserNameResult
	{
		
		private string _Name;
		
		public uspGetUserNameResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(101) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this._Name = value;
				}
			}
		}
	}
	
	public partial class uspLatestUserResult
	{
		
		private string _Column1;
		
		public uspLatestUserResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="", Storage="_Column1", DbType="NChar(13)")]
		public string Column1
		{
			get
			{
				return this._Column1;
			}
			set
			{
				if ((this._Column1 != value))
				{
					this._Column1 = value;
				}
			}
		}
	}
	
	public partial class uspLoginResult
	{
		
		private string _UserID;
		
		public uspLoginResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", DbType="NChar(13) NOT NULL", CanBeNull=false)]
		public string UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				if ((this._UserID != value))
				{
					this._UserID = value;
				}
			}
		}
	}
	
	public partial class uspRetrieveBalanceResult
	{
		
		private int _UserBalance;
		
		public uspRetrieveBalanceResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserBalance", DbType="Int NOT NULL")]
		public int UserBalance
		{
			get
			{
				return this._UserBalance;
			}
			set
			{
				if ((this._UserBalance != value))
				{
					this._UserBalance = value;
				}
			}
		}
	}
	
	public partial class uspCheckActive2Result
	{
		
		private int _MachineID;
		
		public uspCheckActive2Result()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MachineID", DbType="Int NOT NULL")]
		public int MachineID
		{
			get
			{
				return this._MachineID;
			}
			set
			{
				if ((this._MachineID != value))
				{
					this._MachineID = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
