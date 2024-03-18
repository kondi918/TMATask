class ItemResponse {
    constructor(ItemName, UnitOfMeasurement, Quantity, PriceWithoutVAT,Comment) {
        this.ItemName = ItemName;
        this.UnitOfMeasurement = UnitOfMeasurement;
        this.Quantity = Quantity;
        this.PriceWithoutVAT = PriceWithoutVAT;
        this.Comment = Comment;
    }
}

export default ItemResponse;
