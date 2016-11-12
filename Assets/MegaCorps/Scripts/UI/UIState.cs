using System;

public class UIState {

    private static UIState instance = null;

    public City activeCity { get; private set; }
    private Player activePlayer;
    private CityBlock activeBlock;

    public event EventHandler<CityChangedEventArgs> CityChanged;
    public event EventHandler<PlayerChangedEventArgs> PlayerChanged;
    public event EventHandler<BlockChangedEventArgs> BlockChanged;

    private UIState() {
    }

    public static UIState getInstance() {
        if (instance == null) {
            instance = new UIState();
        }

        return instance;
    }
    
    public void setActiveCity(City city) {
        this.activeCity = city;

        if (CityChanged != null) {
            CityChangedEventArgs args = new CityChangedEventArgs();
            args.city = this.activeCity;
            CityChanged.Invoke(this, args);
        }

        this.setActivePlayer(this.activeCity.getCurrentPlayer());
    }

    public Player getActivePlayer() {
        return this.activePlayer;
    }

    public void setActivePlayer(Player player) {
        Player previousPlayer = this.activePlayer;

        this.activePlayer = player;

        if (PlayerChanged != null) {
            PlayerChangedEventArgs args = new PlayerChangedEventArgs();
            args.player = this.activePlayer;
            args.previousPlayer = previousPlayer;
            PlayerChanged.Invoke(this, args);
        }
    }

    public CityBlock getActiveBlock() {
        return this.activeBlock;
    }

    public void setActiveBlock(CityBlock block) {
        CityBlock previousBlock = this.activeBlock;
        this.activeBlock = block;

        if (BlockChanged != null) {
            BlockChangedEventArgs args = new BlockChangedEventArgs();
            args.block = this.activeBlock;
            args.previousBlock = previousBlock;
            this.BlockChanged.Invoke(this, args);
        }
    }

    public class CityChangedEventArgs : EventArgs {
        public City city { get; set; }
    }

    public class PlayerChangedEventArgs : EventArgs {
        public Player player { get; set; }
        public Player previousPlayer { get; set; }
    }

    public class BlockChangedEventArgs : EventArgs {
        public CityBlock block { get; set; }
        public CityBlock previousBlock { get; set; }
    }
}
