class UpdateUserRequest {
    constructor(Id, Username, Password, Role) {
        this.Id = Id;
        this.Username = Username;
        this.Password = Password;
        this.Role = Role
    }
}

export default UpdateUserRequest