/**
 * Export all available controllers.
 */
const account = require('./account-controller');
const version = require('./version-controller');

module.exports = {
  account,
  version,
};
