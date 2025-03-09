using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Menu;
using CounterStrikeSharp.API.Modules.Utils;

namespace SamurayInfoPlugin
{
    [MinimumApiVersion(80)] // Минимальная версия API для CS2
    public class SamurayInfoPlugin : BasePlugin
    {
        public override string ModuleName => "SamurayInfoPlugin";
        public override string ModuleVersion => "2.0.0";
        public override string ModuleAuthor => "samuray69";
        public override string ModuleDescription => "Displays information about server developers in a centered menu with WASD navigation when !devs is typed in CS2.";

        public override void Load(bool hotReload)
        {
            // Регистрируем команду !devs
            AddCommand("devs", "Show information about server developers", HandleDevsCommand);

            Server.PrintToConsole("Samuray Info Plugin loaded for CS2!");
            AddCommand("css_debug", "Debug plugin status", (player, info) =>
            {
                if (player == null) Server.PrintToConsole("Plugin is loaded and active.");
                else player.PrintToChat("Plugin is loaded and active.");
            });
        }

        private void HandleDevsCommand(CCSPlayerController? player, CommandInfo command)
        {
            // Если команда вызвана из консоли сервера (player == null)
            if (player == null)
            {
                Server.PrintToConsole("Информация о разработчиках доступна только в игре через !devs.");
                return;
            }

            // Создаем текстовое меню
            var menu = new ChatMenu("[Burberry] Информация о разработчиках\n Используйте 1-5 для выбора (WASD имитация)");

            // Добавляем опции с номерами и Discord-тегами в обработчиках
            menu.AddMenuOption("1. ➤ Разработчик Discord: samuray666", (p, o) => { p.PrintToChat("Discord разработчика: samuray666"); });
            menu.AddMenuOption("2. ➤ Куратор Discord: viktoriia08", (p, o) => { p.PrintToChat("Discord куратора: viktoriia08"); });
            menu.AddMenuOption("3. ➤ Спонсор Discord: senya982", (p, o) => { p.PrintToChat("Discord спонсора: senya982"); });
            menu.AddMenuOption("4. ➤ Донат: Весь на 30 дней (discord/steam)", (p, o) => { p.PrintToChat("Донат доступен через Discord/Steam. Свяжитесь с samuray666 или viktoriia08!"); });
            menu.AddMenuOption("5. ➤ Спасибо за поддержку сервера!", (p, o) => { p.PrintToChat("Спасибо за поддержку! Свяжитесь с нами в Discord: samuray666, viktoriia08, senya982"); });

            // Отображаем меню игроку
            try
            {
                MenuManager.OpenChatMenu(player, menu);
                Server.PrintToConsole($"[SamurayInfoPlugin] Menu opened for player {player.PlayerName}");
            }
            catch (Exception ex)
            {
                Server.PrintToConsole($"[SamurayInfoPlugin] Error opening menu: {ex.Message}");
                // Fallback на PrintToChat
                player.PrintToChat("=== [Burberry] Информация о разработчиках ===");
                player.PrintToChat("1. ➤ Разработчик Discord: samuray666");
                player.PrintToChat("2. ➤ Куратор Discord: viktoriia08");
                player.PrintToChat("3. ➤ Спонсор Discord: senya982");
                player.PrintToChat("4. ➤ Донат: Весь на 30 дней (discord/steam)");
                player.PrintToChat("5. ➤ Спасибо за поддержку сервера!");
                player.PrintToChat("==============================");
            }
        }
    }
}