using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
namespace Game.Sprites
{
    [CreateAssetMenu]
    public class SpriteData : SpriteDataBase
    {
        
        public int SpriteId;
        [SerializeField] private Sprite sprite;
        [SerializeField] private SpriteAtlas spriteAtlas;
        protected override Sprite Sprite()
        {
            return sprite;
        }

        protected override SpriteAtlas SpriteAtlas()
        {
            return spriteAtlas;
        }

    }



    public enum UISpinner
    {
        SpinBtn,
        CurrentBg,
        BlueBtn,
        GreenBtn,
        OrangeBtn,
        BronzSpin,
        BronzIndicator,
        SilverSpin,
        SilverIndicator,
        GoldSpin,
        GoldIndicator

    }

   

    public enum UIOthers
    {
        UIFrame,
        UIcardZonelWhite,
        UIcardZoneBg,
        UIcardZoneSuper
    }

   

    public enum UIDeath
    {
        
    }

}


