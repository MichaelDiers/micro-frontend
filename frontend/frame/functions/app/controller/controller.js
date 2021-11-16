/**
 * Export all available controllers.
 */
const account = require('./account-controller');
const error = require('./error-controller');
const frame = require('./frame-controller');
const license = require('./license-controller');
const pictureCredit = require('./picture-credit-controller');

module.exports = {
  account,
  error,
  frame,
  license,
  pictureCredit,
};
