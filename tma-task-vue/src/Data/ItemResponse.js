class ItemResponse {
    constructor(ItemID, ItemGroup, UnitOfMeasurement, Quantity, PriceWithoutVAT, Status, StorageLocation, ContactPerson, Photo) {
        this.ItemID = ItemID;
        this.ItemGroup = ItemGroup;
        this.UnitOfMeasurement = UnitOfMeasurement;
        this.Quantity = Quantity;
        this.PriceWithoutVAT = PriceWithoutVAT;
        this.Status = Status;
        this.StorageLocation = StorageLocation;
        this.ContactPerson = ContactPerson;
        this.Photo = Photo;
    }
}

export default ItemResponse;
