using System.Collections.Generic;
using System.Threading.Tasks;
using MembersCard.Entities;

namespace MembersCard.Interfaces
{
    public interface IUserInfoService
    {
        /// <summary>
        /// 新規ユーザーの作成
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<User> CreateNewUserAsync(string userId);

        /// <summary>
        /// データ取得
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <returns>会員ユーザー情報</returns>
        Task<User> GetAsync(string userId);

        /// <summary>
        /// データ登録
        /// </summary>
        /// <param name="user">ユーザー情報</param>
        Task PutAsync(User user);

        /// <summary>
        /// ポイントと期限日を更新する
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="point"></param>
        /// <param name="expirationDate"></param>
        Task UpdatePointExpirationDateAsync(string userId, int point, string expirationDate);

        Task<List<long>> QueryIndexBarcodeNum(long barcodeNum);
    }
}
