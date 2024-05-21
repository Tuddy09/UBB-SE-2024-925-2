namespace BoardGames.Model.CommonEntities;
using Board_games.Model.Interfaces;

public class Pawn : IPawn
{
    public int Id { get; set; }
    public ITile OccupiedTile { get; set; }
    public Player AssociatedPlayer { get; set; }


    public Pawn()
    {
    }
    public Pawn(int pawnId, Tile occupiedTile)
    {
        Id = pawnId;
        this.OccupiedTile = occupiedTile;
    }

    public Pawn(int pawnId, ITile occupiedTile, Player associatedPlayer)
    {
        Id = pawnId;
        this.OccupiedTile = occupiedTile;
        this.AssociatedPlayer = associatedPlayer;
    }

    public void ChangeTile(Tile tileToChangeTo)
    {
        OccupiedTile = tileToChangeTo;
    }
    public ITile GetOccupiedTile()
    {
        return OccupiedTile;
    }
    public int GetPawnId()
    {
        return Id;
    }
    public Player GetPlayer()
    {
        return AssociatedPlayer;
    }

    public void SetAssociatedPlayer(Player associatedPlayer)
    {
        this.AssociatedPlayer = associatedPlayer;
    }

    public Player GetAssociatedPlayer()
    {
        return AssociatedPlayer;
    }
}