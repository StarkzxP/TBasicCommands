  
using Terraria;
using Terraria.ModLoader;

namespace ExampleMod.Commands
{
    public class ItemCommand : ModCommand
    {
        public override CommandType Type => CommandType.Console;
        public override string Command => "item";
        public override string Usage => "/item <PlayerName> <ItemName> (<Stack>)\nReplace spaces in item name with underscores";
        public override string Description => "Spawn an item";

        public override void Action(CommandCaller caller, string input, string[] args) {
            if (!int.TryParse(args[0], out int type)) {
                int player;
                for (player = 0; player < 255; player++) {
                    if (Main.player[player].active && Main.player[player].name.ToLower() == args[0].ToLower()) {
                        break;
                    }
                }
                if (player == 255) {
                    throw new UsageException("Could not find player: " + args[0]);
                }

                var name = args[1].Replace("_", " ").ToLower();
                var item = new Item();
                for (var k = 0; k < ItemLoader.ItemCount; k++) {
                    item.SetDefaults(k, true);
                    if (name != Lang.GetItemNameValue(k).ToLower()) {
                        continue;
                    }

                    type = k;
                    break;
                }

                if (type == 0) {
                    throw new UsageException("Unknown item: " + name);
                }

                int stack = 1;
                if (args.Length >= 2) {
                    stack = int.Parse(args[2]);
                }

                Main.player[player].QuickSpawnItem(type, stack);
            }
        }
    }
}