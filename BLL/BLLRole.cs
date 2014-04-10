using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;
using SAC.Helper;

namespace BLL
{
    public class BLLRole
    {
        DALRole dr = new DALRole();
        DateHelper pb = new DateHelper();
        #region 角色管理
        //获取所有角色信息
        public DataTable GetRoleList()
        {
            return dr.GetRoleList();
        }
        //返回所有角色信息
        public DataTable GetAllRole(int sCount, int eCount)
        {
            return dr.GetAllRole(sCount, eCount);
        }
        //返回所有角色信息的条数
        public int GetRoleCount()
        {
            return dr.GetRoleCount();
        }
        //保存新的角色信息
        public bool SaveRole(string rId, string rName, out string errMsg)
        {
            return dr.SaveRole(rId, rName, out errMsg);
        }
        //编辑原有的角色信息
        public bool UpDateRole(string OrId, string rId, string rName, out string errMsg)
        {
            return dr.UpDateRole(OrId, rId, rName, out errMsg);
        }
        //删除原有的角色信息
        public bool DeleteRole(string rId, out string errMsg)
        {
            return dr.DeleteRole(rId, out errMsg);
        }
        #endregion

        #region 权限管理
        
        public IList<Hashtable> GetMembers()
        {
            return pb.DataTableToList(dr.GetMembers());
        }
        //判断某个岗位下面是否存在人员
        public bool JudgMemberByORGId(string id)
        {
            return dr.JudgMemberByORGId(id);
        }
        #endregion

        #region 角色人员管理

        #region 根据每页显示多少条数据，角色ID返回用户信息
        public DataTable GetUserMenuByRole(string roleId, int sCount, int eCount)
        {
            return dr.GetUserMenuByRole(roleId, sCount, eCount);
        }
        #endregion
        #region 根据角色ID返回所有用户信息的条数
        public int GetUserCountByRole(string roleId)
        {
            return dr.GetUserCountByRole(roleId);
        }
        #endregion
        #region 根据用户名判断是否存在该人员
        public bool JudgMember(string userId)
        {
            return dr.JudgMember(userId);
        }
        #endregion
        #region 添加人员
        public bool AddMember(string id, string name, string pwd, byte[] img, string orgID)
        {
            return dr.AddMember(id, name, pwd, img, orgID);
        }
        #endregion
        #region 返回人员信息
        public IList<Hashtable> GetmemberInfo(string id, int i)
        {
            return pb.DataTableToList(dr.GetmemberInfo(id, i));
        }
        #endregion
        #region 编辑人员信息
        public bool EditMemberInfo(string userIDO, string userID, string userName, string pwd, byte[] img, string treeNodeId)
        {
            return dr.EditMemberInfo(userIDO, userID, userName, pwd, img, treeNodeId);
        }
        #endregion
        #region 删除人员
        /// <summary>
        /// 删除人员信息
        /// </summary>
        /// <param name="id">人员编码</param>
        /// <returns></returns>
        public bool RemoveMember(string id)
        {
            return dr.RemoveMember(id);
        }
        #endregion
        #endregion

        #region 菜单管理
        public bool UpdataWebMenuXml(byte[] fileBytes)
        {
            return dr.UpdataWebMenuXml(fileBytes);
        }
        public bool IsEmptyXml()
        {
            return dr.IsEmptyXml();
        }
        public bool DownLoadXml(string fileID, string filePath)
        {
            return dr.DownLoadXml(fileID, filePath);
        }
        #endregion

        #region 登陆使用
        public DataTable GetUserInfo(string userName)
        {
            return dr.GetUserInfo(userName);
        }
        public string GetRoleId(string userId)
        {
            return dr.GetRoleId(userId);
        }
        #endregion

        #region 管理班次
        //根据每页显示多少条数据返回班次信息
        public DataTable GetBCmenu(int sCount, int eCount)
        {
            return dr.GetBCmenu(sCount, eCount);
        }

        //返回所有班次信息的条数
        public int GetBCCount()
        {
            return dr.GetBCCount();
        }

        //保存新添加的班次信息
        public bool SaveBC(string BcMs, string BcNM, string ST, string ET, out string errMsg)
        {
            return dr.SaveBC(BcMs, BcNM, ST, ET, out errMsg);
        }

        //编辑原来的班次信息
        public bool UpDateBC(string OBcMs, string BcMs, string BcNM, string ST, string ET, out string errMsg)
        {
            return dr.UpadteBC(OBcMs, BcMs, BcNM, ST, ET, out errMsg);
        }

        //删除原来的班次信息
        public bool DeleteBC(int BcId, out string errMsg)
        {
            return dr.DeleteBC(BcId, out errMsg);
        }
        #endregion

        #region 管理班组
        //根据每页显示多少条数据返回职别（班组）信息
        public DataTable GetBZmenu(int sCount, int eCount)
        {
            return dr.GetBZmenu(sCount, eCount);
        }

        //返回所有职别（班组）信息的条数
        public int GetBZCount()
        {
            return dr.GetBZCount();
        }
        //添加新的职别（班组）信息
        public bool SaveBZ(string BzId, string BzMs, out string errMsg)
        {
            return dr.InsertBZ(BzId, BzMs, out errMsg);
        }

        //编辑原有的职别（班组）信息
        public bool UpDateBZ(string OBzId, string BzId, string BzMs, out string errMsg)
        {
            return dr.UpdateBZ(OBzId, BzId, BzMs, out errMsg);
        }

        //删除原有的职别（班组）信息
        public bool DeleteBZ(string BzId, out string errMsg)
        {
            return dr.DeleteBZ(BzId, out errMsg);
        }
        #endregion

        #region 管理排班
        //得到所有班次的信息列表
        public ArrayList BClist(out string errMsg)
        {
            return dr.BClist(out errMsg);
        }

        //得到所有职别（班组）的信息列表
        public ArrayList BZlist(out string errMsg)
        {
            return dr.BZlist(out errMsg);
        }

        //根据职别（班组）描述得到职别（班组）的编号
        public string BZIDbyBZMS(string bzms, out string errMsg)
        {
            return dr.BZIDbyBZMS(bzms, out errMsg);
        }

        //清空T_SYS_DUTY表
        public bool EmptyPB(out string errMsg)
        {
            return dr.EmptyPB(out errMsg);
        }

        public bool InsertPB(string sqldb2, string sqlsql, out string errMsg)
        {
            return dr.InsertPB(sqldb2, sqlsql, out errMsg);
        }
        #endregion
    }
}
