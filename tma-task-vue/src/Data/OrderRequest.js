class OrderRequest {
    constructor(ItemID, Name, UnitOfMeasurement, Quantity, PriceWithoutVAT,Comment,EmployeeName) {
        this.Name = Name;
        this.UnitOfMeasurement = UnitOfMeasurement;
        this.Quantity = Quantity;
        this.PriceWithoutVAT = PriceWithoutVAT;
        this.Comment = Comment;
        this.EmployeeName = EmployeeName;
        this.ItemID = ItemID
    }
}

export default OrderRequest;
