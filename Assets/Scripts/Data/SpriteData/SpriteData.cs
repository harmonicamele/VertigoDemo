using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
namespace Game.Sprites
{
    [CreateAssetMenu]
    public class SpriteData : SpriteDataBase
    {

        [SerializeField] private SpriteAtlas spriteAtlas;
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


