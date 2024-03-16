class User {
    constructor(username, role) {
      this.username = username;
      this.role = role;
    }
    
    hasOPPermission() {
      if(this.role == "OP" || this.role == "ADM") {
        return true;
      }
      return false;
    }
    hasADMPermission() {
        if(this.role == "ADM") {
            return true;
        }
        return false;
    }
  }
  
  export default User;