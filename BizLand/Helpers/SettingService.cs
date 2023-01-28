using BizLand.DAL;
using BizLand.Models;
using Microsoft.EntityFrameworkCore;

namespace BizLand.Helpers
{
    public class SettingService
    {
        private readonly BizLandContext _context;

        public SettingService(BizLandContext context)
        {
            _context = context;
        }

        public async Task<List<Setting>> GetSettingAsync()
        {
            return await _context.Settings.ToListAsync();
        }
    }
}
