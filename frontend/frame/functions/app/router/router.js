const account = require('./account-router');
const error = require('./error-router');
const frame = require('./frame-router');
const license = require('./license-router');
const pictureCredit = require('./picture-credit-router');

module.exports = {
  account,
  error,
  frame,
  license,
  pictureCredit,
};
