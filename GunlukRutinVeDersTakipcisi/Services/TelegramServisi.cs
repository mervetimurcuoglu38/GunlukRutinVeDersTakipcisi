using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace GunlukRutinVeDersTakipcisi.Services
{
    public class TelegramServisi
    {
        private readonly string _botToken;
        private readonly long _chatId;
        private readonly TelegramBotClient _botClient;
        
        public TelegramServisi(string botToken,long chatId)
        {
            _botToken = botToken;
            _chatId = chatId;
            _botClient = new TelegramBotClient(_botToken);

        }
        public async Task<bool> MesajGonderAsync(string mesaj)
        {
            if (string.IsNullOrWhiteSpace(mesaj))
                return false;

            try
            {
                await _botClient.SendMessage(_chatId, mesaj);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
