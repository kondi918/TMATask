class RequestRowsResponse {
    constructor(itemID, itemName, priceWithoutVAT, quantity, unitOfMeasurement) {
        this.itemID = itemID;
        this.itemName = itemName;
        this.priceWithoutVAT = priceWithoutVAT,
        this.quantity = quantity,
        this.unitOfMeasurement = unitOfMeasurement
    }
}

export default RequestRowsResponse;