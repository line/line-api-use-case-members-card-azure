using System.Threading.Tasks;
using MembersCard.Entities;

namespace MembersCard.Interfaces
{
    /// <summary>
    /// LineChannelデータを扱うサービスインターフェース
    /// </summary>
    public interface ILineChannelService
    {
        /// <summary>
        /// データ取得
        /// </summary>
        /// <returns><see cref="LineChannel"/></returns>
        Task<LineChannel> GetAsync();

        /// <summary>
        /// LINEチャネルデータの更新
        /// </summary>
        /// <param name="lineChannel">更新に使用するLINEチャネルデータ</param>
        /// <returns>更新後のLINEチャネルデータ</returns>
        Task<LineChannel> UpsertAsync(LineChannel lineChannel);
    }
}
