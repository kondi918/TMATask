class RejectRequest {
    constructor(RequestID, Status, Comment) {
        this.RequestID = RequestID;
        this.Comment = Comment;
        this.Status = Status;
    }
}

export default RejectRequest;
