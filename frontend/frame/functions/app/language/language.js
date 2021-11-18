const de = require('./language-de.json');
const en = require('./language-en.json');
const fr = require('./language-fr.json');

const translations = (language) => {
  if (language === 'de') {
    return de;
  }

  if (language === 'en') {
    return en;
  }

  if (language === 'fr') {
    return fr;
  }

  return {};
};

const translate = (language, key) => {
  try {
    return translations(language)[key];
  } catch {
    return 'unknown';
  }
};

module.exports = {
  de: 'de',
  default: 'de',
  en: 'en',
  fr: 'fr',
  supported: ['de', 'en', 'fr'],
  translate,
  translations,
};
