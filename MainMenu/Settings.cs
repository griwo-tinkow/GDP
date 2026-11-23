using Godot;
using System;

public partial class Settings : Control
{
    private HSlider _master;
    private HSlider _music;
    private HSlider _sfx;
    private OptionButton _songSelect;
    private AnimationPlayer _fadeIn;
    private Button _return;

    private int _masterBus;
    private int _musicBus;
    private int _sfxBus;

    public override void _Ready()
    {
        _master = GetNode<HSlider>("%MasterVolume");
        _music = GetNode<HSlider>("%MusicVolume");
        _sfx = GetNode<HSlider>("%SFXVolume");
        _songSelect = GetNode<OptionButton>("%SongSelect");
        _fadeIn = GetNode<AnimationPlayer>("%anim");
        _return = GetNode<Button>("%return");


        _masterBus = AudioServer.GetBusIndex("Master");
        _musicBus = AudioServer.GetBusIndex("Music");
        _sfxBus = AudioServer.GetBusIndex("SFX");

        float defaultLinear = 0.8f;

        SetupSlider(_master, _masterBus, defaultLinear);
        SetupSlider(_music, _musicBus, defaultLinear);
        SetupSlider(_sfx, _sfxBus, defaultLinear);

        _fadeIn.Play("fading");
        _return.Pressed += ReturnToMainMenu;
        _songSelect.Select(AudioManager.Instance.CurrentSong);
        _songSelect.ItemSelected += OnSongSelected;
    }

    private void SetupSlider(HSlider slider, int busIndex, float defaultValue)
    {
        if (slider == null) return;

        // Force slider range from 0.0 to 1.0 so it respects percentage dragging
        slider.MinValue = 0.0f;
        slider.MaxValue = 1.0f;
        slider.Step = 0.01f;

        // If the audio server volume is already set, read it; otherwise, default to 80%
        float currentDb = AudioServer.GetBusVolumeDb(busIndex);
        if (currentDb == 0f && slider.Value == 0f)
        {
            slider.Value = defaultValue;
            SetBusVolume(busIndex, defaultValue);
        }
        else
        {
            slider.Value = Mathf.DbToLinear(currentDb);
        }

        if (slider == _master) slider.ValueChanged += OnMasterChanged;
        else if (slider == _music) slider.ValueChanged += OnMusicChanged;
        else if (slider == _sfx) slider.ValueChanged += OnSfxChanged;
    }

    private void OnMasterChanged(double value) { SetBusVolume(_masterBus, (float)value); AudioManager.Instance.PlayBuy(); }
    private void OnMusicChanged(double value) { SetBusVolume(_musicBus, (float)value); AudioManager.Instance.PlayBuy(); }
    private void OnSfxChanged(double value) { SetBusVolume(_sfxBus, (float)value); AudioManager.Instance.PlayBuy(); }

    private void SetBusVolume(int busIndex, float linearValue)
    {
        float db = linearValue > 0f ? Mathf.LinearToDb(linearValue) : -80f;
        AudioServer.SetBusVolumeDb(busIndex, db);
    }

    private async void ReturnToMainMenu()
    {
        _fadeIn.PlayBackwards("fading");
        AudioManager.Instance.PlayBuy();
        await ToSignal(_fadeIn, AnimationPlayer.SignalName.AnimationFinished);
        GetTree().ChangeSceneToFile("res://MainMenu/MainMenu.tscn");
    }
    private void OnSongSelected(long index)
    {
        AudioManager.Instance.PlayMusic((int)index);
    }
}
