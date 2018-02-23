using Entities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PersitanceHelper
    {
        private Database DataBase
        {
            get
            {
                return DatabaseFactory.CreateDatabase("RMSDB");
            }
        }

        #region Device

        public bool AddDevice(string Name, string OutFreq, string DcBusVolt, string OutputCurr, string InputPow
          , string OutputVolt, string OutputFreq, string DcBusVoltageFormat, string OutputCurrForm, string inptPwrFor, string OutVoltFor,
            string BaudRate, string Parity, string StopBit)
        {
            try
            {
                var Command = DataBase.GetStoredProcCommand("usp_Ins_Device");
                DataBase.AddInParameter(Command, "@Name", DbType.String, Name);
                DataBase.AddInParameter(Command, "@OutFreq", DbType.String, OutFreq);
                DataBase.AddInParameter(Command, "@DcBusVolt", DbType.String, DcBusVolt);
                DataBase.AddInParameter(Command, "@OutputCurr", DbType.String, OutputCurr);
                DataBase.AddInParameter(Command, "@InputPow", DbType.String, InputPow);
                DataBase.AddInParameter(Command, "@OutputVolt", DbType.String, OutputVolt);
                DataBase.AddInParameter(Command, "@OutputFreq", DbType.String, OutputFreq);
                DataBase.AddInParameter(Command, "@DcBusVoltageFormat", DbType.String, DcBusVoltageFormat);
                DataBase.AddInParameter(Command, "@OutputCurrForm", DbType.String, OutputCurrForm);
                DataBase.AddInParameter(Command, "@inptPwrFor", DbType.String, inptPwrFor);
                DataBase.AddInParameter(Command, "@OutVoltFor", DbType.String, OutVoltFor);
                DataBase.AddInParameter(Command, "@BaudRate", DbType.String, BaudRate);
                DataBase.AddInParameter(Command, "@Parity", DbType.String, Parity);
                DataBase.AddInParameter(Command, "@StopBit", DbType.String, StopBit);
                DataBase.ExecuteNonQuery(Command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Device> GetDevices()
        {
            List<Device> Devices = new List<Device>();
            DbCommand cmd = DataBase.GetStoredProcCommand("usp_get_devices");
            using (IDataReader reader = DataBase.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Device Device = new Device();
                    Device.Id = int.Parse(reader["DeviceID"].ToString());
                    Device.Name = reader["Name"].ToString();
                    Device.OutPutFrequency = reader["OutputFrequency"].ToString();
                    Device.DCBusVoltage = reader["DcBusVoltage"].ToString();
                    Device.OutPutCurrent = reader["OutPutCurrent"].ToString();
                    Device.InputPower = reader["InputPower"].ToString();
                    Device.OutPutVoltage = reader["OutPutVoltage"].ToString();
                    Device.OutPutFreqFormat = reader["OutPutFreqFormat"].ToString();
                    Device.DcBusVoltageFormat = reader["DcBusVolFormat"].ToString();
                    Device.OutPutCurrentFormat = reader["OutPutCurrentFormat"].ToString();
                    Device.InputPowerFormat = reader["InputPowerFormat"].ToString();
                    Device.OutPutVolatageFormat = reader["OutPutVoltageFormat"].ToString();
                    Device.BaudRate = reader["BaudRate"].ToString();
                    Device.Parity = reader["Parity"].ToString();
                    Device.StopBit = reader["StopBit"].ToString();
                    Devices.Add(Device);
                }
            }
            return Devices;
        }

        public List<Device> GetDeviceModels()
        {
            List<Device> Devices = new List<Device>();
            DbCommand cmd = DataBase.GetStoredProcCommand("usp_get_devices");
            using (IDataReader reader = DataBase.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Device Device = new Device();
                    Device.Id = int.Parse(reader["DeviceID"].ToString());
                    Device.Name = reader["Name"].ToString();
                    Devices.Add(Device);
                }
            }
            return Devices;
        }

        public bool UpdateDevices(int id, string Name, string OutFreq, string DcBusVolt, string OutputCurr, string InputPow
          , string OutputVolt, string OutputFreq, string DcBusVoltageFormat, string OutputCurrForm, string inptPwrFor, string OutVoltFor,
            string BaudRate, string Parity, string StopBit)
        {
            try
            {
                var Command = DataBase.GetStoredProcCommand("usp_upd_Device");
                DataBase.AddInParameter(Command, "@id", DbType.Int32, id);
                DataBase.AddInParameter(Command, "@Name", DbType.String, Name);
                DataBase.AddInParameter(Command, "@OutFreq", DbType.String, OutFreq);
                DataBase.AddInParameter(Command, "@DcBusVolt", DbType.String, DcBusVolt);
                DataBase.AddInParameter(Command, "@OutputCurr", DbType.String, OutputCurr);
                DataBase.AddInParameter(Command, "@InputPow", DbType.String, InputPow);
                DataBase.AddInParameter(Command, "@OutputVolt", DbType.String, OutputVolt);
                DataBase.AddInParameter(Command, "@OutputFreq", DbType.String, OutputFreq);
                DataBase.AddInParameter(Command, "@DcBusVoltageFormat", DbType.String, DcBusVoltageFormat);
                DataBase.AddInParameter(Command, "@OutputCurrForm", DbType.String, OutputCurrForm);
                DataBase.AddInParameter(Command, "@inptPwrFor", DbType.String, inptPwrFor);
                DataBase.AddInParameter(Command, "@OutVoltFor", DbType.String, OutVoltFor);
                DataBase.AddInParameter(Command, "@BaudRate", DbType.String, BaudRate);
                DataBase.AddInParameter(Command, "@Parity", DbType.String, Parity);
                DataBase.AddInParameter(Command, "@StopBit", DbType.String, StopBit);
                DataBase.ExecuteNonQuery(Command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteDevice(int id)
        {
            try
            {
                var Command = DataBase.GetStoredProcCommand("usp_del_device");
                DataBase.AddInParameter(Command, "@id", DbType.Int32, id);
                DataBase.ExecuteNonQuery(Command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Device

        #region Registered Device

        public bool AddRegisteredDevice(string Name, string DeviceId, string IMEI, string ModelId, bool Enable, string MotorEfficiency, string Head, string MotorPower, bool MotorControl, string BenificiaryName, string WorkOrderNo, string District, string Phone, string VFD, string Address, string interval)
        {
            try
            {
                var Command = DataBase.GetStoredProcCommand("usp_Ins_RegisteredDevice");
                DataBase.AddInParameter(Command, "@Name", DbType.String, Name);
                DataBase.AddInParameter(Command, "@DeviceID", DbType.String, DeviceId);
                DataBase.AddInParameter(Command, "@IMEI", DbType.String, IMEI);
                DataBase.AddInParameter(Command, "@DeviceModelId", DbType.String, ModelId);
                DataBase.AddInParameter(Command, "@Enabled", DbType.Boolean, Enable);
                DataBase.AddInParameter(Command, "@MotorEfficency", DbType.String, MotorEfficiency);
                DataBase.AddInParameter(Command, "@Head", DbType.String, Head);
                DataBase.AddInParameter(Command, "@MoterPower", DbType.String, MotorPower);
                DataBase.AddInParameter(Command, "@MotorControl", DbType.String, MotorControl);
                DataBase.AddInParameter(Command, "@BenificeryName", DbType.String, BenificiaryName);
                DataBase.AddInParameter(Command, "@WorkOrderNo", DbType.String, WorkOrderNo);
                DataBase.AddInParameter(Command, "@District", DbType.String, District);
                DataBase.AddInParameter(Command, "@Phone", DbType.String, Phone);
                DataBase.AddInParameter(Command, "@VFD", DbType.String, VFD);
                DataBase.AddInParameter(Command, "@Address", DbType.String, Address);
                DataBase.AddInParameter(Command, "@Interval", DbType.String, interval);
                DataBase.ExecuteNonQuery(Command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RegisteredDevice> GetRegisteredDevices()
        {
            List<RegisteredDevice> RegisteredDevices = new List<RegisteredDevice>();
            DbCommand cmd = DataBase.GetStoredProcCommand("usp_get_Registereddevices");
            using (IDataReader reader = DataBase.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    RegisteredDevice RegisteredDevice = new RegisteredDevice();
                    RegisteredDevice.Id = int.Parse(reader["ID"].ToString());
                    RegisteredDevice.Name = reader["Name"].ToString();
                    RegisteredDevice.DeviceId = reader["DeviceID"].ToString();
                    RegisteredDevice.IMEI = reader["IMEI"].ToString();
                    RegisteredDevice.RegisteredDeviceId = reader["DeviceModelId"].ToString();
                    RegisteredDevice.Enabled = bool.Parse(reader["Enabled"].ToString());
                    RegisteredDevice.MotorEfficiency = reader["MotorEfficiency"].ToString();
                    RegisteredDevice.Head = reader["Head"].ToString();
                    RegisteredDevice.MotorPower = reader["MotorPower"].ToString();
                    RegisteredDevice.MotorControl = bool.Parse(reader["MotorControl"].ToString());
                    RegisteredDevice.BenificiaryName = reader["BenificaryName"].ToString();
                    RegisteredDevice.WorkOrderNo = reader["workOrderNo"].ToString();
                    RegisteredDevice.District = reader["District"].ToString();
                    RegisteredDevice.Phone = reader["Phone"].ToString();
                    RegisteredDevice.VFD = reader["VFD"].ToString();
                    RegisteredDevice.Address = reader["Address"].ToString();
                    RegisteredDevice.Interval = reader["Interval"].ToString();
                    RegisteredDevice.DistributorName = reader["DistributorName"].ToString();
                    RegisteredDevice.CompanyName = reader["CompanyName"].ToString();
                    RegisteredDevice.Lan = reader["Lan"].ToString();
                    RegisteredDevice.Lon = reader["Lon"].ToString();
                    RegisteredDevice.Ccid = reader["CcId"].ToString();
                    RegisteredDevice.OpName = reader["Opame"].ToString();
                    RegisteredDevice.Signal = reader["Signal"].ToString();
                    RegisteredDevice.Serial = reader["Serial"].ToString();
                    RegisteredDevice.STime = reader["Stime"].ToString();
                    RegisteredDevice.Btime = reader["Btime"].ToString();
                    RegisteredDevices.Add(RegisteredDevice);
                }
            }
            return RegisteredDevices;
        }

        public bool UpdateRegisteredDevices(int Id, string Name, string DeviceId, string IMEI, string ModelId, bool Enable, string MotorEfficiency, string Head, string MotorPower, bool MotorControl, string BenificiaryName, string WorkOrderNo, string District, string Phone, string VFD, string Address)
        {
            try
            {
                var Command = DataBase.GetStoredProcCommand("usp_upd_RegisterdDevice");
                DataBase.AddInParameter(Command, "@id", DbType.Int32, Id);
                DataBase.AddInParameter(Command, "@Name", DbType.String, Name);
                DataBase.AddInParameter(Command, "@DeviceID", DbType.String, DeviceId);
                DataBase.AddInParameter(Command, "@IMEI", DbType.String, IMEI);
                DataBase.AddInParameter(Command, "@DeviceModelId", DbType.String, ModelId);
                DataBase.AddInParameter(Command, "@Enabled", DbType.Boolean, Enable);
                DataBase.AddInParameter(Command, "@MotorEfficency", DbType.String, MotorEfficiency);
                DataBase.AddInParameter(Command, "@Head", DbType.String, Head);
                DataBase.AddInParameter(Command, "@MoterPower", DbType.String, MotorPower);
                DataBase.AddInParameter(Command, "@MotorControl", DbType.Boolean, MotorControl);
                DataBase.AddInParameter(Command, "@BenificeryName", DbType.String, BenificiaryName);
                DataBase.AddInParameter(Command, "@WorkOrderNo", DbType.String, WorkOrderNo);
                DataBase.AddInParameter(Command, "@District", DbType.String, District);
                DataBase.AddInParameter(Command, "@Phone", DbType.String, Phone);
                DataBase.AddInParameter(Command, "@VFD", DbType.String, VFD);
                DataBase.AddInParameter(Command, "@Address", DbType.String, Address);
                DataBase.ExecuteNonQuery(Command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // private usp_Ins_RegisteredDevice

        public DetailAPIResponse GetDeviceDetailAPI(string imei, string lat, string lon, string ccid, string opname, string signal, string serial,
            string stime, string btime)
        {
            DetailAPIResponse Device = null;
            DbCommand cmd = DataBase.GetStoredProcCommand("usp_get_RegisteredDevice_Api");
            DataBase.AddInParameter(cmd, "@imei", DbType.String, imei);
            DataBase.AddInParameter(cmd, "@lat", DbType.String, lat);
            DataBase.AddInParameter(cmd, "@lon", DbType.String, lon);
            DataBase.AddInParameter(cmd, "@ccid", DbType.String, ccid);
            DataBase.AddInParameter(cmd, "@opname", DbType.String, opname);
            DataBase.AddInParameter(cmd, "@signal", DbType.String, signal);
            DataBase.AddInParameter(cmd, "@serial", DbType.String, serial);
            DataBase.AddInParameter(cmd, "@stime", DbType.String, stime);
            DataBase.AddInParameter(cmd, "@btime", DbType.String, btime);
            using (IDataReader reader = DataBase.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Device = new DetailAPIResponse();
                    Device.imei = imei;
                    Device.device_id = int.Parse(reader["DeviceID"].ToString());
                    Device.output_frequency = int.Parse(reader["OutputFrequency"].ToString());
                    Device.dc_bus_voltage = int.Parse(reader["DcBusVoltage"].ToString());
                    Device.output_current = int.Parse(reader["OutPutCurrent"].ToString());
                    Device.input_power = int.Parse(reader["InputPower"].ToString());
                    Device.output_voltage = int.Parse(reader["OutPutVoltage"].ToString());
                    Device.output_freq_multi = int.Parse(reader["OutPutFreqFormat"].ToString());
                    Device.dc_bus_voltage_multi = int.Parse(reader["DcBusVolFormat"].ToString());
                    Device.output_current_multi = int.Parse(reader["OutPutCurrentFormat"].ToString());
                    Device.input_power_multi = int.Parse(reader["InputPowerFormat"].ToString());
                    Device.output_voltage_multi = int.Parse(reader["OutPutVoltageFormat"].ToString());
                    Device.interval = int.Parse(reader["Interval"].ToString());
                    Device.baud = int.Parse(reader["BaudRate"].ToString());
                    Device.parity = int.Parse(reader["Parity"].ToString());
                    Device.stopbit = int.Parse(reader["StopBit"].ToString());
                }
            }
            return Device;
        }

        public bool DeleteRegsiteredDevice(int id)
        {
            try
            {
                var Command = DataBase.GetStoredProcCommand("usp_del_registereddevice");
                DataBase.AddInParameter(Command, "@id", DbType.Int32, id);
                DataBase.ExecuteNonQuery(Command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Registered Device

        #region Login/SignUp

        public int RegisterUser(RMSUser user)
        {
            int UserID = 0;
            try
            {
                var Command = DataBase.GetStoredProcCommand("usp_Ins_RegisterUser");
                DataBase.AddInParameter(Command, "@FirstName", DbType.String, user.FirstName);
                DataBase.AddInParameter(Command, "@LastName", DbType.String, user.LastName);
                DataBase.AddInParameter(Command, "@UserName", DbType.String, user.UserName);
                DataBase.AddInParameter(Command, "@Password", DbType.String, user.Password);
                DataBase.AddInParameter(Command, "@PhoneNumber", DbType.String, user.PhoneNumber);
                DataBase.AddInParameter(Command, "@Email", DbType.String, user.Email);
                DataBase.AddInParameter(Command, "@Role", DbType.String, user.RoleID);

                using (IDataReader reader = DataBase.ExecuteReader(Command))
                {
                    while (reader.Read())
                    {
                        UserID = int.Parse(reader["UserID"].ToString());
                    }
                }
                return UserID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RMSUser ValidateUser(string UserName, string Password)
        {
            RMSUser user = null;
            DbCommand cmd = DataBase.GetStoredProcCommand("usp_validateUser");
            DataBase.AddInParameter(cmd, "@UserName", DbType.String, UserName);
            DataBase.AddInParameter(cmd, "@Password", DbType.String, Password);
            using (IDataReader reader = DataBase.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    user = new RMSUser();
                    user.Id = int.Parse(reader["ID"].ToString());
                    user.FirstName = reader["FirstName"].ToString();
                    user.LastName = reader["LastName"].ToString();
                    user.UserName = reader["UserName"].ToString();
                    user.Password = reader["Password"].ToString();
                    user.Email = reader["Email"].ToString();
                    user.RoleID = reader["Role"].ToString();
                    user.RoleName = reader["Name"].ToString();
                }
            }
            return user;
        }

        public List<Roles> GetRoles()
        {
            List<Roles> Roles = new List<Roles>();
            DbCommand cmd = DataBase.GetStoredProcCommand("usp_get_roles");

            using (IDataReader reader = DataBase.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Roles Role = new Roles();
                    Role.Id = int.Parse(reader["ID"].ToString());
                    Role.RoleName = reader["Name"].ToString();
                    Roles.Add(Role);
                }
            }
            return Roles;
        }

        public List<RMSUser> GetUsers(int roleId, string userIDs)
        {
            List<RMSUser> Users = new List<RMSUser>();
            DbCommand cmd = DataBase.GetStoredProcCommand("usp_get__users");
            DataBase.AddInParameter(cmd, "@roleid", DbType.Int32, roleId);
            DataBase.AddInParameter(cmd, "@userIds", DbType.String, userIDs);
            using (IDataReader reader = DataBase.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    RMSUser User = new RMSUser();
                    User.Id = int.Parse(reader["ID"].ToString());
                    User.FirstName = reader["FirstName"].ToString() + " " + reader["LastName"].ToString();
                    User.LastName = reader["LastName"].ToString();
                    User.UserName = reader["UserName"].ToString();
                    User.Password = reader["Password"].ToString();
                    User.Email = reader["Email"].ToString();
                    User.RoleID = reader["Role"].ToString();
                    User.PhoneNumber = reader["PhoneNumber"].ToString();
                    User.RoleName = reader["Name"].ToString();
                    Users.Add(User);
                }
            }
            return Users;
        }

        public bool CompanyUserMapping(int compId, int userID, int typeID)
        {
            try
            {
                var Command = DataBase.GetStoredProcCommand("usp_Ins_Company_User_Mapping");
                DataBase.AddInParameter(Command, "@CompId", DbType.String, compId);
                DataBase.AddInParameter(Command, "@userId", DbType.String, userID);
                DataBase.AddInParameter(Command, "@typeId", DbType.String, typeID);
                DataBase.ExecuteNonQuery(Command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteUser(int id)
        {
            try
            {
                var Command = DataBase.GetStoredProcCommand("usp_del_User");
                DataBase.AddInParameter(Command, "@id", DbType.Int32, id);
                DataBase.ExecuteNonQuery(Command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Login/SignUp

        #region DistributorCompany

        public List<Distributor> GetDistributor()
        {
            List<Distributor> Distributors = new List<Distributor>();
            DbCommand cmd = DataBase.GetStoredProcCommand("usp_get_distributor");

            using (IDataReader reader = DataBase.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Distributor Distributor = new Distributor();
                    Distributor.Id = int.Parse(reader["ID"].ToString());
                    Distributor.DistributorName = reader["UserName"].ToString();
                    Distributors.Add(Distributor);
                }
            }
            return Distributors;
        }

        public bool InsertDistributor(string distrName)
        {
            try
            {
                var Command = DataBase.GetStoredProcCommand("usp_Ins_Distributor");
                DataBase.AddInParameter(Command, "@DistributorName", DbType.String, distrName);

                DataBase.ExecuteNonQuery(Command);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            //
        }

        #endregion DistributorCompany

        #region Company

        public List<Company> GetCompany(int distID)
        {
            List<Company> Companies = new List<Company>();
            DbCommand cmd = DataBase.GetStoredProcCommand("usp_get_comapny");
            DataBase.AddInParameter(cmd, "@DistId", DbType.Int32, distID);
            using (IDataReader reader = DataBase.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Company company = new Company();
                    company.Id = int.Parse(reader["ID"].ToString());
                    company.CompanyName = reader["CompanyName"].ToString();
                    company.DistributorID = int.Parse(reader["DistributorId"].ToString());
                    company.LogoPath = reader["LogoPath"].ToString();
                    company.OrganizationName = reader["OrgName"].ToString();
                    company.Address = reader["Address"].ToString();
                    company.Email = reader["Email"].ToString();
                    company.PhoneNumber = reader["PhoneNumber"].ToString();
                    Companies.Add(company);
                }
            }
            return Companies;
        }

        public bool InsertCompany(string compName, int distID)
        {
            try
            {
                var Command = DataBase.GetStoredProcCommand("usp_Ins_Company");
                DataBase.AddInParameter(Command, "@CompanyName", DbType.String, compName);
                DataBase.AddInParameter(Command, "@DistID", DbType.Int32, distID);
                DataBase.ExecuteNonQuery(Command);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            //
        }

        public bool InsertCompany(string compName, int distID, string logoPath, string orgName, string address, string email, string phoneNumber)
        {
            try
            {
                var Command = DataBase.GetStoredProcCommand("usp_Ins_Company");
                DataBase.AddInParameter(Command, "@CompanyName", DbType.String, compName);
                DataBase.AddInParameter(Command, "@DistID", DbType.Int32, distID);
                DataBase.AddInParameter(Command, "@LogoPath", DbType.String, logoPath);
                DataBase.AddInParameter(Command, "@OrgName", DbType.String, orgName);
                DataBase.AddInParameter(Command, "@Address", DbType.String, address);
                DataBase.AddInParameter(Command, "@Email", DbType.String, email);
                DataBase.AddInParameter(Command, "@PhoneNumber", DbType.String, phoneNumber);
                DataBase.ExecuteNonQuery(Command);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<RegisteredDevice> GetUnAssignedDevices()
        {
            List<RegisteredDevice> UnAssignedDevices = new List<RegisteredDevice>();
            DbCommand cmd = DataBase.GetStoredProcCommand("usp_get_unAssignedDevices");

            using (IDataReader reader = DataBase.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    RegisteredDevice Devices = new RegisteredDevice();
                    Devices.Id = int.Parse(reader["ID"].ToString());
                    Devices.Name = reader["Name"].ToString();
                    UnAssignedDevices.Add(Devices);
                }
            }
            return UnAssignedDevices;
        }

        public bool UpdateUnassignedDevices(int deviceID, int distID, int compID)
        {
            try
            {
                var Command = DataBase.GetStoredProcCommand("usp_upd_unAssignedDevices");
                DataBase.AddInParameter(Command, "@deviceID", DbType.Int32, deviceID);
                DataBase.AddInParameter(Command, "@DistributorId", DbType.Int32, distID);
                DataBase.AddInParameter(Command, "@CompanyId", DbType.Int32, compID);
                DataBase.ExecuteNonQuery(Command);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            //
        }

        public bool DeleteCompany(int id)
        {
            try
            {
                var Command = DataBase.GetStoredProcCommand("usp_del_Company");
                DataBase.AddInParameter(Command, "@id", DbType.Int32, id);
                DataBase.ExecuteNonQuery(Command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateCompany(int id, string CompName, int distId, string logo, string orgName, string address, string email, string phNum)
        {
            try
            {
                var Command = DataBase.GetStoredProcCommand("[usp_upd_Company]");
                DataBase.AddInParameter(Command, "@id", DbType.Int32, id);
                DataBase.AddInParameter(Command, "@CompName", DbType.String, CompName);
                DataBase.AddInParameter(Command, "@DistId", DbType.Int32, distId);
                DataBase.AddInParameter(Command, "@Logo", DbType.String, logo);
                DataBase.AddInParameter(Command, "@OrgName", DbType.String, orgName);
                DataBase.AddInParameter(Command, "@Address", DbType.String, address);
                DataBase.AddInParameter(Command, "@Email", DbType.String, email);
                DataBase.AddInParameter(Command, "@PhoneNum", DbType.String, phNum);

                DataBase.ExecuteNonQuery(Command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Company

        #region LiveQueue

        public RebootReset InsertLiveQueue(LiveQueue liveQueue)
        {
            try
            {
                RebootReset RebootReset = new RebootReset();
                var Command = DataBase.GetStoredProcCommand("usp_ins_LiveQueue_Api");
                DataBase.AddInParameter(Command, "@imei", DbType.String, liveQueue.imei);
                DataBase.AddInParameter(Command, "@deviceid", DbType.Int32, liveQueue.device_id);
                DataBase.AddInParameter(Command, "@frequency", DbType.Decimal, liveQueue.frequency);
                DataBase.AddInParameter(Command, "@dcbusvoltage", DbType.Decimal, liveQueue.dc_bus_voltage);
                DataBase.AddInParameter(Command, "@current", DbType.Decimal, liveQueue.current);
                DataBase.AddInParameter(Command, "@inpower", DbType.Decimal, liveQueue.in_power);
                DataBase.AddInParameter(Command, "@outvoltage", DbType.Decimal, liveQueue.output_voltage);
                using (IDataReader reader = DataBase.ExecuteReader(Command))
                {
                    while (reader.Read())
                    {
                        RebootReset.Reboot = reader["Reboot"].ToString().ToLowerInvariant().Equals("true") ? "true" : "false";
                        RebootReset.Reset = reader["ResetDevice"].ToString().ToLowerInvariant().Equals("true") ? "true" : "false";
                    }
                }

                return RebootReset;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<RegisteredDevice> GetAssignedDevices(int distributorId, int companyId)
        {
            List<RegisteredDevice> AssignedDevices = new List<RegisteredDevice>();
            DbCommand cmd = DataBase.GetStoredProcCommand("usp_get_AssignedDevices");
            DataBase.AddInParameter(cmd, "@DistributorId", DbType.String, distributorId);
            DataBase.AddInParameter(cmd, "@CompanyId", DbType.String, companyId);

            using (IDataReader reader = DataBase.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    RegisteredDevice Devices = new RegisteredDevice();
                    Devices.Id = int.Parse(reader["ID"].ToString());
                    Devices.Name = reader["Name"].ToString();
                    Devices.IMEI = reader["IMEI"].ToString();
                    AssignedDevices.Add(Devices);
                }
            }
            return AssignedDevices;
        }

        public List<RegisteredDevice> GetDistributorDevices(int distributorId)
        {
            List<RegisteredDevice> AssignedDevices = new List<RegisteredDevice>();
            DbCommand cmd = DataBase.GetStoredProcCommand("usp_get_AssignedDevices_farmer");
            DataBase.AddInParameter(cmd, "@DistributorId", DbType.String, distributorId);
            using (IDataReader reader = DataBase.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    RegisteredDevice Devices = new RegisteredDevice();
                    Devices.Id = int.Parse(reader["ID"].ToString());
                    Devices.Name = reader["Name"].ToString();
                    Devices.IMEI = reader["IMEI"].ToString();
                    AssignedDevices.Add(Devices);
                }
            }
            return AssignedDevices;
        }

        public LiveQueue GetLiveQueue(string imei)
        {
            LiveQueue Queue = new LiveQueue();
            DbCommand cmd = DataBase.GetStoredProcCommand("usp_get_LiveQueue_Api");
            DataBase.AddInParameter(cmd, "@imei", DbType.String, imei);

            using (IDataReader reader = DataBase.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Queue.imei = reader["IMEI"].ToString();
                    Queue.device_id = int.Parse(reader["DeviceId"].ToString());
                    Queue.frequency = decimal.Parse(reader["Frequency"].ToString());
                    Queue.dc_bus_voltage = decimal.Parse(reader["Dc_Bus_Voltage"].ToString());
                    Queue.current = decimal.Parse(reader["Currents"].ToString());
                    Queue.in_power = decimal.Parse(reader["In_Power"].ToString());
                    Queue.output_voltage = decimal.Parse(reader["Output_Voltage"].ToString());
                    Queue.last_update = DateTime.Parse(reader["Last_Update"].ToString());
                }
                reader.NextResult();
                {
                    while (reader.Read())
                    {
                        Queue.longitude = reader["Lon"].ToString();
                        Queue.lattitude = reader["Lan"].ToString();
                    }
                }
            }
            return Queue;
        }

        public List<LiveQueue> GetLiveQueueBulk(string imei)
        {
            List<LiveQueue> Queues = new List<LiveQueue>();
            DbCommand cmd = DataBase.GetStoredProcCommand("usp_get_LiveQueue_Api_Blk");
            DataBase.AddInParameter(cmd, "@imei", DbType.String, imei);

            using (IDataReader reader = DataBase.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    LiveQueue Queue = new LiveQueue();
                    Queue.imei = reader["IMEI"].ToString();
                    Queue.device_id = int.Parse(reader["DeviceId"].ToString());
                    Queue.frequency = decimal.Parse(reader["Frequency"].ToString());
                    Queue.dc_bus_voltage = decimal.Parse(reader["Dc_Bus_Voltage"].ToString());
                    Queue.current = decimal.Parse(reader["Currents"].ToString());
                    Queue.in_power = decimal.Parse(reader["In_Power"].ToString());
                    Queue.output_voltage = decimal.Parse(reader["Output_Voltage"].ToString());
                    Queue.last_update = Convert.ToDateTime(reader["Last_Update"].ToString());
                    Queues.Add(Queue);
                }
            }
            return Queues;
        }

        public List<LiveQueue> GetLiveQueueBulkReport(string imei, DateTime startDate, DateTime endDate)
        {
            List<LiveQueue> Queues = new List<LiveQueue>();
            DbCommand cmd = DataBase.GetStoredProcCommand("usp_get_LiveQueue_Api_Report");
            DataBase.AddInParameter(cmd, "@imei", DbType.String, imei);
            DataBase.AddInParameter(cmd, "@start", DbType.String, startDate.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture));
            DataBase.AddInParameter(cmd, "@end", DbType.String, endDate.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture));

            using (IDataReader reader = DataBase.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    LiveQueue Queue = new LiveQueue();
                    Queue.imei = reader["IMEI"].ToString();
                    Queue.device_id = int.Parse(reader["ID"].ToString());
                    Queue.frequency = decimal.Parse(reader["Frequency"].ToString());
                    Queue.dc_bus_voltage = decimal.Parse(reader["Dc_Bus_Voltage"].ToString());
                    Queue.current = decimal.Parse(reader["Currents"].ToString());
                    Queue.in_power = decimal.Parse(reader["In_Power"].ToString());
                    Queue.output_voltage = decimal.Parse(reader["Output_Voltage"].ToString());
                    Queue.last_update = Convert.ToDateTime(reader["Last_Update"].ToString());
                    Queues.Add(Queue);
                }
            }
            return Queues;
        }

        public bool UpdateRebootReset(string imei, bool reboot, bool reset)
        {
            try
            {
                var Command = DataBase.GetStoredProcCommand("usp_upd_Reset_Reboot");
                DataBase.AddInParameter(Command, "@imei", DbType.String, imei);
                DataBase.AddInParameter(Command, "@reboot", DbType.Boolean, reboot);
                DataBase.AddInParameter(Command, "@reset", DbType.Boolean, reset);
                DataBase.ExecuteNonQuery(Command);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion LiveQueue

        #region State Village Mandal

        public bool AddState(string stateName)
        {
            try
            {
                var Command = DataBase.GetStoredProcCommand("usp_Ins_State");
                DataBase.AddInParameter(Command, "@StateName", DbType.String, stateName);
                DataBase.ExecuteNonQuery(Command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DelState(int stateId)
        {
            try
            {
                var Command = DataBase.GetStoredProcCommand("usp_del_state");
                DataBase.AddInParameter(Command, "@id", DbType.Int32, stateId);
                DataBase.ExecuteNonQuery(Command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<State> GetStates()
        {
            List<State> States = new List<State>();

            DbCommand cmd = DataBase.GetStoredProcCommand("usp_get_States");
            using (IDataReader reader = DataBase.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    State State = new State();
                    State.Id = int.Parse(reader["ID"].ToString());
                    State.StateName = reader["StateName"].ToString();
                    States.Add(State);
                }
            }
            return States;
        }

        public bool AddVillage(string villageName, int stateId)
        {
            try
            {
                var Command = DataBase.GetStoredProcCommand("usp_Ins_Village");
                DataBase.AddInParameter(Command, "@VillageName", DbType.String, villageName);
                DataBase.AddInParameter(Command, "@StateId", DbType.Int32, stateId);
                DataBase.ExecuteNonQuery(Command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DelVillage(int villageId)
        {
            try
            {
                var Command = DataBase.GetStoredProcCommand("usp_del_village");
                DataBase.AddInParameter(Command, "@id", DbType.Int32, villageId);
                DataBase.ExecuteNonQuery(Command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Village> GetVillages()
        {
            List<Village> Villages = new List<Village>();

            DbCommand cmd = DataBase.GetStoredProcCommand("usp_get_village");
            using (IDataReader reader = DataBase.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Village Village = new Village();
                    Village.Id = int.Parse(reader["ID"].ToString());
                    Village.VillageName = reader["VillageName"].ToString();
                    Village.StateId = int.Parse(reader["StateId"].ToString());
                    Villages.Add(Village);
                }
            }
            return Villages;
        }

        public List<Village> GetVillages(int Id)
        {
            List<Village> Villages = new List<Village>();

            DbCommand cmd = DataBase.GetStoredProcCommand("usp_get_village_id");
            DataBase.AddInParameter(cmd, "@Stateid", DbType.Int32, Id);
            using (IDataReader reader = DataBase.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Village Village = new Village();
                    Village.Id = int.Parse(reader["ID"].ToString());
                    Village.VillageName = reader["VillageName"].ToString();
                    Village.StateId = int.Parse(reader["StateId"].ToString());
                    Villages.Add(Village);
                }
            }
            return Villages;
        }

        public bool AddMandal(string mandalName, int villageId, int stateId)
        {
            try
            {
                var Command = DataBase.GetStoredProcCommand("usp_Ins_Mandal");
                DataBase.AddInParameter(Command, "@MandalName", DbType.String, mandalName);
                DataBase.AddInParameter(Command, "@VillageId", DbType.Int32, villageId);
                DataBase.AddInParameter(Command, "@StateId", DbType.Int32, stateId);
                DataBase.ExecuteNonQuery(Command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DelMandal(int mandalId)
        {
            try
            {
                var Command = DataBase.GetStoredProcCommand("usp_del_mandal");
                DataBase.AddInParameter(Command, "@id", DbType.Int32, mandalId);
                DataBase.ExecuteNonQuery(Command);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Mandal> GetMandals()
        {
            List<Mandal> Mandals = new List<Mandal>();

            DbCommand cmd = DataBase.GetStoredProcCommand("usp_get_mandal");
            using (IDataReader reader = DataBase.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Mandal Mandal = new Mandal();
                    Mandal.Id = int.Parse(reader["ID"].ToString());
                    Mandal.MandalName = reader["MandalName"].ToString();
                    Mandal.VillageId = int.Parse(reader["VillageId"].ToString());
                    Mandal.StateId = int.Parse(reader["StateId"].ToString());
                    Mandals.Add(Mandal);
                }
            }
            return Mandals;
        }

        #endregion State Village Mandal

        #region Farmer

        public bool InsertFarmer(string farmerName, int deviceID, string pono, string districtId, string phoneNumber
            , int stateId, int villageID, int mandalId, string department, string lattitude, string longitude, int distId, int compID)
        {
            try
            {
                var Command = DataBase.GetStoredProcCommand("usp_Ins_farmer");
                DataBase.AddInParameter(Command, "@farmerName", DbType.String, farmerName);
                DataBase.AddInParameter(Command, "@deviceid", DbType.Int32, deviceID);
                DataBase.AddInParameter(Command, "@pono", DbType.String, pono);
                DataBase.AddInParameter(Command, "@districtId", DbType.String, districtId);
                DataBase.AddInParameter(Command, "@phonenumber", DbType.String, phoneNumber);
                DataBase.AddInParameter(Command, "@stateId", DbType.String, stateId);
                DataBase.AddInParameter(Command, "@villageID", DbType.String, villageID);
                DataBase.AddInParameter(Command, "@mandnalId", DbType.String, mandalId);
                DataBase.AddInParameter(Command, "@department", DbType.String, department);
                DataBase.AddInParameter(Command, "@lattitude", DbType.String, lattitude);
                DataBase.AddInParameter(Command, "@longitude", DbType.String, longitude);
                DataBase.AddInParameter(Command, "@distID", DbType.Int32, distId);
                DataBase.AddInParameter(Command, "@compID", DbType.Int32, compID);
                DataBase.ExecuteNonQuery(Command);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            //
        }

        public List<Farmer> GetFarmers(int distID)
        {
            List<Farmer> Farmers = new List<Farmer>();
            DbCommand cmd = DataBase.GetStoredProcCommand("usp_get_Farmer");
            DataBase.AddInParameter(cmd, "@DistId", DbType.Int32, distID);

            using (IDataReader reader = DataBase.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    Farmer Farmer = new Farmer();
                    Farmer.Id = int.Parse(reader["FarmerID"].ToString());
                    Farmer.FarmerName = reader["FarmerName"].ToString();
                    Farmer.DeviceId = int.Parse(reader["DeviceId"].ToString());
                    Farmer.Pono = reader["Pono"].ToString();
                    Farmer.DistrictId = reader["DistrictId"].ToString();
                    Farmer.PhoneNumber = reader["PhoneNumber"].ToString();
                    Farmer.StateId = int.Parse(reader["StateId"].ToString());
                    Farmer.VillageId = int.Parse(reader["VillageId"].ToString());
                    Farmer.MadnalId = int.Parse(reader["MandalId"].ToString());
                    Farmer.Department = reader["Department"].ToString();
                    Farmer.Lattitude = reader["Lattitude"].ToString();
                    Farmer.Longitude = reader["Longitude"].ToString();
                    Farmer.DistId = int.Parse(reader["DistributorID"].ToString());
                    //Farmer.Id = int.Parse(reader["RegisterdDeviceID"].ToString());
                    Farmer.Name = reader["Name"].ToString();
                    // Farmer.DeviceId = reader["DeviceID"].ToString();
                    Farmer.IMEI = reader["IMEI"].ToString();
                    Farmer.RegisteredDeviceId = reader["DeviceModelId"].ToString();
                    Farmer.Enabled = bool.Parse(reader["Enabled"].ToString());
                    Farmer.MotorEfficiency = reader["MotorEfficiency"].ToString();
                    Farmer.Head = reader["Head"].ToString();
                    Farmer.MotorPower = reader["MotorPower"].ToString();
                    Farmer.MotorControl = bool.Parse(reader["MotorControl"].ToString());
                    Farmer.BenificiaryName = reader["BenificaryName"].ToString();
                    Farmer.WorkOrderNo = reader["workOrderNo"].ToString();
                    Farmer.District = reader["District"].ToString();
                    Farmer.Phone = reader["Phone"].ToString();
                    Farmer.VFD = reader["VFD"].ToString();
                    Farmer.Address = reader["Address"].ToString();
                    Farmer.Interval = reader["Interval"].ToString();
                    Farmer.DistributorName = reader["DistributorId"].ToString();
                    Farmer.CompanyName = reader["CompanyID"].ToString();
                    Farmer.DistributorFirstLastName = reader["FirstName"].ToString() + " " + reader["LastName"].ToString();
                    Farmer.FarmerCompanyName = reader["CompanyName"].ToString();

                    Farmer.Lan = reader["Lan"].ToString();
                    Farmer.Lon = reader["Lon"].ToString();
                    Farmer.Ccid = reader["CcId"].ToString();
                    Farmer.OpName = reader["Opame"].ToString();
                    Farmer.Signal = reader["Signal"].ToString();
                    Farmer.Serial = reader["Serial"].ToString();
                    Farmer.STime = reader["Stime"].ToString();
                    Farmer.Btime = reader["Btime"].ToString();

                    Farmers.Add(Farmer);
                }
            }
            return Farmers;
        }

        public List<int> GetUserIdsbyCompanyID(string compID)
        {
            List<int> UserIDs = new List<int>();
            DbCommand cmd = DataBase.GetStoredProcCommand("usp_get_Usercomapny_Mapping");
            DataBase.AddInParameter(cmd, "@CompIDs", DbType.String, compID);

            using (IDataReader reader = DataBase.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    int userid = int.Parse(reader["UserID"].ToString());
                    UserIDs.Add(userid);
                }
            }
            return UserIDs;
        }

        #endregion Farmer

        public int GetCompanyId(int userId)
        {
            int CompanyId = 0;
            DbCommand cmd = DataBase.GetStoredProcCommand("usp_get_CompanyIdbyUserID");
            DataBase.AddInParameter(cmd, "@UserId", DbType.Int32, userId);
            using (IDataReader reader = DataBase.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CompanyId = int.Parse(reader["CompID"].ToString());
                }
            }
            return CompanyId;
        }
    }
}