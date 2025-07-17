using Godot;
using System;

public partial class SettingsButton : TextureButton
{
    public void _on_SettingsButton_pressed()
    {
        GetTree().ChangeSceneToFile("res://settings_Menu.tscn");
    }
}
