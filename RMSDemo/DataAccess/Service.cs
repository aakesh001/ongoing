using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Service
    {
        public void AddDevice(string Name, string OutFreq, string DcBusVolt, string OutputCurr, string InputPow
           , string OutputVolt, string OutputFreq, string DcBusVoltageFormat, string OutputCurrForm, string inptPwrFor, string OutVoltFor, string BaudRate, string Parity, string StopBit)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            PersitanceHelper.AddDevice(Name, OutFreq, DcBusVolt, OutputCurr, InputPow, OutputVolt, OutputFreq, DcBusVoltageFormat, OutputCurrForm, inptPwrFor, OutVoltFor,
                  BaudRate, Parity, StopBit);
        }

        public List<Device> GetDevices()
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.GetDevices();
        }

        public bool UpdateDevice(int id, string Name, string OutFreq, string DcBusVolt, string OutputCurr, string InputPow
           , string OutputVolt, string OutputFreq, string DcBusVoltageFormat, string OutputCurrForm, string inptPwrFor, string OutVoltFor, string BaudRate, string Parity, string StopBit)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.UpdateDevices(id, Name, OutFreq, DcBusVolt, OutputCurr, InputPow
           , OutputVolt, OutputFreq, DcBusVoltageFormat, OutputCurrForm, inptPwrFor, OutVoltFor, BaudRate, Parity, StopBit);
        }

        public List<Device> GetDeviceModels()
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.GetDeviceModels();
        }

        public bool DeleteDevices(int id)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.DeleteDevice(id);
        }

        public List<RegisteredDevice> GetRegisteredDevices()
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.GetRegisteredDevices();
        }

        public bool AddRegisteredDevice(string Name, string DeviceId, string IMEI, string ModelId, bool Enable, string MotorEfficiency, string Head,
          string MotorPower, bool MotorControl, string BenificiaryName, string WorkOrderNo, string District, string Phone, string VFD, string Address, string Interval)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.AddRegisteredDevice(Name, DeviceId, IMEI, ModelId, Enable, MotorEfficiency, Head, MotorPower,
             MotorControl, BenificiaryName, WorkOrderNo, District, Phone, VFD, Address, Interval);
        }

        public bool UpdateRegisteredDevice(int Id, string Name, string DeviceId, string IMEI, string ModelId, bool Enable, string MotorEfficiency, string Head,
          string MotorPower, bool MotorControl, string BenificiaryName, string WorkOrderNo, string District, string Phone, string VFD, string Address)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.UpdateRegisteredDevices(Id, Name, DeviceId, IMEI, ModelId, Enable, MotorEfficiency, Head,
           MotorPower, MotorControl, BenificiaryName, WorkOrderNo, District, Phone, VFD, Address);
        }

        public DetailAPIResponse GetDeviceDetailAPI(string imei, string lat, string lon, string ccid, string opname, string signal, string serial,
            string stime, string btime)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.GetDeviceDetailAPI(imei, lat, lon, ccid, opname, signal, serial, stime, btime);
        }

        public bool DeleteRegisteredDevices(int id)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.DeleteRegsiteredDevice(id);
        }

        #region Login/Signup

        public int RegsiterUser(RMSUser user)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.RegisterUser(user);
        }

        public RMSUser ValidateUser(string UserName, string Password)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.ValidateUser(UserName, Password);
        }

        public List<Roles> GetRoles()
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.GetRoles();
        }

        public List<RMSUser> GetUSers(int roleId, string userIds)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.GetUsers(roleId, userIds);
        }

        public bool CompanyUserMapping(int compId, int userID, int typeID)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.CompanyUserMapping(compId, userID, typeID);
        }

        public bool DeleteUser(int id)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.DeleteUser(id);
        }

        #endregion Login/Signup

        #region DistComapny

        public List<Distributor> GetDistributor()
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.GetDistributor();
        }

        public bool InsertDistributor(string distrName)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.InsertDistributor(distrName);
        }

        public List<Company> GetCompany(int distID)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.GetCompany(distID);
        }

        public List<int> GetUserIDbyCompanyID(string CompIDs)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.GetUserIdsbyCompanyID(CompIDs);
        }

        public bool InsertCompany(string compName, int distID)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.InsertCompany(compName, distID);
        }

        public bool InsertCompany(string compName, int distID, string logoPath, string orgName, string address, string email, string phoneNumber)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.InsertCompany(compName, distID, logoPath, orgName, address, email, phoneNumber);
        }

        public List<RegisteredDevice> GetUnAssignedDevice()
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.GetUnAssignedDevices();
        }

        public bool UpdateUnAssignedDevice(int deviceId, int distributorID, int companyId)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.UpdateUnassignedDevices(deviceId, distributorID, companyId);
        }

        public bool DeleteCompany(int id)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.DeleteCompany(id);
        }

        public bool UpdateCompany(int id, string CompName, int distId, string logo, string orgName, string address, string email, string phNum)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.UpdateCompany(id, CompName, distId, logo, orgName, address, email, phNum);
        }

        #endregion DistComapny

        #region Live Queue

        public RebootReset InsertLiveQueue(LiveQueue liveQueue)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.InsertLiveQueue(liveQueue);
        }

        public List<RegisteredDevice> GetAssignedDevice(int distId, int compID)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.GetAssignedDevices(distId, compID);
        }

        public LiveQueue GetLiveQueue(string imei)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.GetLiveQueue(imei);
        }

        public List<LiveQueue> GetLiveQueueBulk(string imei)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.GetLiveQueueBulk(imei);
        }

        public List<LiveQueue> GetLiveQueueBulkReport(string imei, DateTime startDate, DateTime endDate)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.GetLiveQueueBulkReport(imei, startDate, endDate);
        }

        public bool UpdateRebootReset(string imei, bool reboot, bool reset)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.UpdateRebootReset(imei, reboot, reset);
        }

        #endregion Live Queue

        #region State Village Mandal

        public bool AddState(string stateName)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.AddState(stateName);
        }

        public List<State> GetStates()
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.GetStates();
        }

        public bool DelState(int stateId)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.DelState(stateId);
        }

        public bool AddVillage(string villageName, int stateId)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.AddVillage(villageName, stateId);
        }

        public bool DelVillage(int villageId)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.DelVillage(villageId);
        }

        public List<Village> GetVillages()
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.GetVillages();
        }

        public List<Village> GetVillages(int Id)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.GetVillages(Id);
        }

        public bool AddMandal(string mandalName, int villageId, int stateId)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.AddMandal(mandalName, villageId, stateId);
        }

        public bool DelMandal(int mandalId)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.DelMandal(mandalId);
        }

        public List<Mandal> GetMandal()
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.GetMandals();
        }

        #endregion State Village Mandal

        #region Farmer

        public List<RegisteredDevice> GetDitributorDevice(int distId)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.GetDistributorDevices(distId);
        }

        public bool InsertFarmer(string farmerName, int deviceID, string pono, string districtId, string phoneNumber
          , int stateId, int villageID, int mandalId, string department, string lattitude, string longitude, int distID, int compID)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.InsertFarmer(farmerName, deviceID, pono, districtId, phoneNumber
          , stateId, villageID, mandalId, department, lattitude, longitude, distID, compID);
        }

        public List<Farmer> GetFarmers(int distID)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.GetFarmers(distID);
        }

        #endregion Farmer

        public int GetCompanyId(int userID)
        {
            PersitanceHelper PersitanceHelper = new PersitanceHelper();
            return PersitanceHelper.GetCompanyId(userID);
        }
    }
}