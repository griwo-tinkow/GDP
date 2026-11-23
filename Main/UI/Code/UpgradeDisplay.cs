using Godot;
using System;
using UpgradeClasses.Upgrades;

public partial class UpgradeDisplay : PanelContainer
{
    public enum UpgradeType
    {
        PriceIncrease,
        ConveyorExpress,
        BombSquad,
        Furnace,
        Tools,
        HotterFire,
        Factory,
        DigDeeper,
        Megaplex,
        Slow,
        Wind,
        End
    }
    [Export] public UpgradeType CurrentUpgrade;
    private static bool _blockedAuto = false;

    private Label _nameLabel;
    private Label _descLabel;
    private Label _levelLabel;
    private TextureRect _icon;
    private Button _upgradeButton;

    private OpenAFactory factory = new OpenAFactory();
    private HotterFire fires = new HotterFire();
    private HireBombSquad bombsquad = new HireBombSquad();
    private IncreaseCosts price = new IncreaseCosts();
    private ImproveTools tools = new ImproveTools();
    private ConveyorSpeed conveyor = new ConveyorSpeed();
    private BuyFurnaces furnace = new BuyFurnaces();
    private MineDeeper mine = new MineDeeper();
    private OpenAMexaplex megaplex = new OpenAMexaplex();
    private SlowDown slow = new SlowDown();
    private WindMills wind = new WindMills();
    private GEA gea = new GEA();




    public override void _Ready()
    {
        _nameLabel = GetNodeOrNull<Label>("%name");
        _levelLabel = GetNodeOrNull<Label>("%level");
        _descLabel = GetNodeOrNull<Label>("%desc");
        _icon = GetNodeOrNull<TextureRect>("%texture");
        _upgradeButton = GetNodeOrNull<Button>("%button");

        _upgradeButton.Pressed += OnUpgradePressed;
        UpdateUI();

    }
    public void BlockButton()
    {
        _upgradeButton.Disabled = true;
        _upgradeButton.Text = "Disabled";
    }
    private void OnUpgradePressed()
    {
        AudioManager.Instance.PlayBuy();
        switch (CurrentUpgrade)
        {
            case UpgradeType.PriceIncrease:
                price.BuyUpgrade();
                break;

            case UpgradeType.ConveyorExpress:
                conveyor.BuyUpgrade();
                break;

            case UpgradeType.BombSquad:
                bombsquad.BuyUpgrade();
                break;

            case UpgradeType.Furnace:
                furnace.BuyUpgrade();
                break;

            case UpgradeType.Tools:
                tools.BuyUpgrade();
                break;

            case UpgradeType.HotterFire:
                fires.BuyUpgrade();
                break;

            case UpgradeType.Factory:
                factory.BuyUpgrade();
                break;
            case UpgradeType.DigDeeper:
                mine.BuyUpgrade();
                break;
            case UpgradeType.Megaplex:
                megaplex.BuyUpgrade();
                break;
            case UpgradeType.Slow:
                slow.BuyUpgrade();
                break;
            case UpgradeType.Wind:
                wind.BuyUpgrade();
                break;
            case UpgradeType.End:
                gea.BuyUpgrade();
                break;
        }
        UpdateUI();
        CheckIfReadyForEcoMode();
    }
    private void CheckIfReadyForEcoMode()
    {
        bool c1 = ConveyorSpeed.Level >= ConveyorSpeed.MaxLevel;
        bool c2 = BuyFurnaces.Level >= BuyFurnaces.MaxLevel;
        bool c3 = ImproveTools.Level >= ImproveTools.MaxLevel;
        bool c4 = HotterFire.Level >= HotterFire.MaxLevel;
        bool c5 = MineDeeper.Level >= MineDeeper.MaxLevel;

        bool i1 = IncreaseCosts.Level >= 15;
        bool i2 = HireBombSquad.Level >= 5;
        bool i3 = OpenAFactory.Level >= 5;
        bool i4 = OpenAMexaplex.Level >= 5;

        bool s = GlobalValues.Instance.Score >= 3000000f;
        bool _ready = c1 && c2 && c3 && c4 && c5 && i1 && i2 && i3 && i4 && s;

        if (_ready) GlobalValues.Instance.EcoMode = true;
    }

    public void UpdateUI()
    {
        switch (CurrentUpgrade)
        {
            case UpgradeType.PriceIncrease:
                _nameLabel.Text = price.UName;
                _icon.Texture = price.Icon;
                _levelLabel.Text = IncreaseCosts.Level + "/inf";
                _descLabel.Text = price.UDesc;
                _upgradeButton.Disabled = false;
                _upgradeButton.Text = GlobalValues.Instance.NormaliseMoney(price.Price);
                break;

            case UpgradeType.ConveyorExpress:
                _nameLabel.Text = conveyor.UName;
                _icon.Texture = conveyor.Icon;
                _levelLabel.Text = ConveyorSpeed.Level + "/" + ConveyorSpeed.MaxLevel;
                _descLabel.Text = conveyor.UDesc;
                if (ConveyorSpeed.Level == ConveyorSpeed.MaxLevel)
                {
                    _upgradeButton.Disabled = true;
                    _upgradeButton.Text = "MAX";
                }
                else
                {
                    _upgradeButton.Disabled = false;
                    _upgradeButton.Text = GlobalValues.Instance.NormaliseMoney(conveyor.Price);
                }
                break;

            case UpgradeType.BombSquad:
                _nameLabel.Text = bombsquad.UName;
                _icon.Texture = bombsquad.Icon;
                _levelLabel.Text = HireBombSquad.Level + "/inf";
                _descLabel.Text = bombsquad.UDesc;
                if (_blockedAuto) _upgradeButton.Text = "Disabled";
                else _upgradeButton.Text = GlobalValues.Instance.NormaliseMoney(bombsquad.Price);
                break;

            case UpgradeType.Furnace:
                _nameLabel.Text = furnace.UName;
                _icon.Texture = furnace.Icon;
                _levelLabel.Text = BuyFurnaces.Level + "/" + BuyFurnaces.MaxLevel;
                _descLabel.Text = furnace.UDesc;
                if (BuyFurnaces.Level == BuyFurnaces.MaxLevel)
                {
                    _upgradeButton.Disabled = true;
                    _upgradeButton.Text = "MAX";
                }
                else
                {
                    _upgradeButton.Disabled = false;
                    _upgradeButton.Text = GlobalValues.Instance.NormaliseMoney(furnace.Price);
                }
                break;

            case UpgradeType.Tools:
                _nameLabel.Text = tools.UName;
                _icon.Texture = tools.Icon;
                _levelLabel.Text = ImproveTools.Level + "/" + ImproveTools.MaxLevel;
                _descLabel.Text = tools.UDesc;
                if (ImproveTools.Level == ImproveTools.MaxLevel)
                {
                    _upgradeButton.Disabled = true;
                    _upgradeButton.Text = "MAX";
                }
                else
                {
                    _upgradeButton.Disabled = false;
                    _upgradeButton.Text = GlobalValues.Instance.NormaliseMoney(tools.Price);
                }
                break;

            case UpgradeType.HotterFire:
                _nameLabel.Text = fires.UName;
                _icon.Texture = fires.Icon;
                _levelLabel.Text = HotterFire.Level + "/" + HotterFire.MaxLevel;
                _descLabel.Text = fires.UDesc;
                if (HotterFire.Level == HotterFire.MaxLevel)
                {
                    _upgradeButton.Disabled = true;
                    _upgradeButton.Text = "MAX";
                }
                else
                {
                    _upgradeButton.Disabled = false;
                    _upgradeButton.Text = GlobalValues.Instance.NormaliseMoney(fires.Price);
                }
                break;

            case UpgradeType.Factory:
                _nameLabel.Text = factory.UName;
                _icon.Texture = factory.Icon;
                _levelLabel.Text = OpenAFactory.Level + "/inf";
                _descLabel.Text = factory.UDesc;
                if (_blockedAuto) _upgradeButton.Text = "Disabled";
                else _upgradeButton.Text = GlobalValues.Instance.NormaliseMoney(factory.Price);
                break;

            case UpgradeType.DigDeeper:
                _nameLabel.Text = mine.UName;
                _icon.Texture = mine.Icon;
                _levelLabel.Text = MineDeeper.Level + "/" + MineDeeper.MaxLevel;
                _descLabel.Text = mine.UDesc;
                if (MineDeeper.Level == MineDeeper.MaxLevel)
                {
                    _upgradeButton.Disabled = true;
                    _upgradeButton.Text = "MAX";
                }
                else
                {
                    _upgradeButton.Disabled = false;
                    _upgradeButton.Text = GlobalValues.Instance.NormaliseMoney(mine.Price);
                }
                break;

            case UpgradeType.Megaplex:
                _nameLabel.Text = megaplex.UName;
                _icon.Texture = megaplex.Icon;
                _levelLabel.Text = OpenAMexaplex.Level + "/inf";
                _descLabel.Text = megaplex.UDesc;
                if (_blockedAuto) _upgradeButton.Text = "Disabled";
                else _upgradeButton.Text = GlobalValues.Instance.NormaliseMoney(megaplex.Price);
                break;

            case UpgradeType.Slow:
                if (GlobalValues.Instance.EcoMode)
                {
                    Visible = true;
                    _nameLabel.Text = slow.UName;
                    _descLabel.Text = slow.UDesc;
                    _icon.Texture = slow.Icon;
                    _levelLabel.Text = slow.Level + "/" + slow.MaxLevel;
                    if (slow.Level == slow.MaxLevel)
                    {
                        _upgradeButton.Disabled = true;
                        _upgradeButton.Text = "DONE";
                        _blockedAuto = true;

                    }
                    else
                    {
                        _upgradeButton.Disabled = false;
                        _upgradeButton.Text = GlobalValues.Instance.NormaliseMoney(slow.Price);
                    }
                }
                else
                {
                    Visible = false;
                }
                break;

            case UpgradeType.Wind:
                if (GlobalValues.Instance.EcoMode)
                {
                    Visible = true;
                    _nameLabel.Text = wind.UName;
                    _descLabel.Text = wind.UDesc;
                    _icon.Texture = wind.Icon;
                    _levelLabel.Text = wind.Level + "/" + wind.MaxLevel;
                    if (slow.Level != slow.MaxLevel)
                    {
                        _upgradeButton.Disabled = true;
                        _upgradeButton.Text = "Top first";
                    }
                    else if (wind.Level == wind.MaxLevel)
                    {
                        _upgradeButton.Disabled = true;
                        _upgradeButton.Text = "DONE";
                    }
                    else
                    {
                        _upgradeButton.Disabled = false;
                        _upgradeButton.Text = GlobalValues.Instance.NormaliseMoney(wind.Price);
                        SignalBus.Instance.EmitSignal(SignalBus.SignalName.BlockFurtherUpgrades);
                    }
                }
                else
                {
                    Visible = false;
                }
                break;

            case UpgradeType.End:
                if (GlobalValues.Instance.EcoMode)
                {
                    Visible = true;
                    _nameLabel.Text = gea.UName;
                    _descLabel.Text = gea.UDesc;
                    _icon.Texture = gea.Icon;
                    _levelLabel.Text = gea.Level + "/" + gea.MaxLevel;
                    if (wind.Level != wind.MaxLevel)
                    {
                        _upgradeButton.Disabled = true;
                        _upgradeButton.Text = "Top first";
                    }
                    else if (gea.Level == gea.MaxLevel)
                    {
                        _upgradeButton.Disabled = true;
                        SignalBus.Instance.EmitSignal(SignalBus.SignalName.WinningUpgradeBought, true);
                    }
                    else
                    {
                        _upgradeButton.Disabled = false;
                        _upgradeButton.Text = GlobalValues.Instance.NormaliseMoney(gea.Price);
                    }
                }
                else
                {
                    Visible = false;
                }
                break;
        }
    }
    public override void _PhysicsProcess(double delta)
    {
        UpdateUI();
    }
}
