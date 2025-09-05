export class Account {
  /**
   * @typedef AccountData
   * @property {string} sub
   * @property {string} email
   * @property {string} name
   * @property {string} picture
   * 
   * @param {AccountData} data
   */
  constructor(data) {
    this.sub = data.sub
    this.email = data.email
    this.name = data.name
    this.picture = data.picture
    // TODO add additional properties if needed
  }
}
