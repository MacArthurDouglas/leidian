using com.yihui.Buttons;
using UnityEngine;


[AddComponentMenu("Buttons/VoiceButton")]
public class VoiceButton : Buttons
{
    private SpriteRenderer player;
    public Sprite pictureVoiceOn;
    public Sprite pictureVoiceOnEnter;
    public Sprite pictureVoiceOff;
    public Sprite pictureVoiceOffEnter;
    private Vector2 mousePos;
    private Vector2 buttonPos;
    private bool voiceOn;

    public override void NextStart()
    {
        player = this.GetComponent<SpriteRenderer>();
        voiceOn = true;
        player.sprite = voiceOn ? pictureVoiceOn : pictureVoiceOff;
        
    }

    public override void MouseEnter()
    {
        if (!voiceOn)
        {
            player.sprite = pictureVoiceOffEnter;
        }
        else
        {
            player.sprite = pictureVoiceOnEnter;
        }
    }

    public override void MouseExit()
    {
        if (!voiceOn)
        {
            player.sprite = pictureVoiceOff;
        }
        else
        {
            player.sprite = pictureVoiceOn;
        }
    }

    public override void OnClick()
    {
        voiceOn = !voiceOn;
    }
}