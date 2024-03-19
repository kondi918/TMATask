class User {
  constructor(username, role) {
      this.username = username;
      this.role = role;
      this.OPPermission = this.hasOPPermission(); // Przypisanie wyniku metody hasOPPermission() do właściwości OPPermission
      this.ADMPermission = this.hasADMPermission(); // Przypisanie wyniku metody hasADMPermission() do właściwości ADMPermission
  }
  
  hasOPPermission() {
      return this.role === "CO" || this.role === "ADM"; // Zwraca true, jeśli role to "CO" lub "ADM", w przeciwnym razie false
  }
  
  hasADMPermission() {
      return this.role === "ADM"; // Zwraca true, jeśli role to "ADM", w przeciwnym razie false
  }
}

export default User;
