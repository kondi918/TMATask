class User {
  constructor(username, role) {
      this.username = username;
      this.role = role;
      this.OPPermission = this.hasOPPermission(); 
      this.ADMPermission = this.hasADMPermission();
  }
  
  hasOPPermission() {
      return this.role === "CO" || this.role === "ADM"; 
  }
  
  hasADMPermission() {
      return this.role === "ADM";
  }
}

export default User;