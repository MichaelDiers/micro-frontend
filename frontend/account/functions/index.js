const functions = require('firebase-functions');

const app = require('./app/app');
const appConfig = require('./app-config');

exports.account = functions.https.onRequest(app(appConfig()));
