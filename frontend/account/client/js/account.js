const accountEvents = async () => {
  const buildUrl = (path) => {
    const language = document.querySelector('html[lang]')?.getAttribute('lang'); // eslint-disable-line no-undef
    let url;
    if (language) {
      url = `${accountAddress.accountBase}/${language}${path}`; // eslint-disable-line no-undef
    } else {
      url = `${accountAddress.accountBase}${path}`; // eslint-disable-line no-undef
    }

    return url;
  };

  const handleEvent = async (url, method, localUrl, targetElement, body) => {
    const target = targetElement;
    return apiCall(url, method, body).then((view) => { // eslint-disable-line no-undef
      target.innerHTML = view.replace(/__LOCAL__URL__/, localUrl);
    }).catch(handleError); // eslint-disable-line no-undef
  };

  document.addEventListener('accountHeaderLinkRequest', (e) => { // eslint-disable-line no-undef
    handleEvent(buildUrl(accountAddress.accountIndex), 'GET', e.detail, e.target).catch(handleError); // eslint-disable-line no-undef
  });

  document.addEventListener('accountFeVersion', (e) => { // eslint-disable-line no-undef
    handleEvent(buildUrl(accountAddress.accountVersion), 'GET', e.detail, e.target).catch(handleError); // eslint-disable-line no-undef
  });

  document.addEventListener('accountlogonRequest', (e) => { // eslint-disable-line no-undef
    handleEvent(buildUrl(accountAddress.accountLogon), 'GET', e.detail, e.target).then(() => { // eslint-disable-line no-undef
      const element = e.target.querySelector('#accountLogonFormSubmit');
      if (element) {
        element.addEventListener('click', (innerEvent) => {
          innerEvent.preventDefault();
          if (formValidation(e.target, '#accountLogonFormEmail', '#accountLogonFormPassword') === true) { // eslint-disable-line no-undef
            const email = e.target.querySelector('#accountLogonFormEmail').value;
            const password = e.target.querySelector('#accountLogonFormPassword').value;
            handleEvent(buildUrl(accountAddress.accountLogon), 'POST', e.detail, e.target, { email, password }).catch(handleError); // eslint-disable-line no-undef
          }
        });
      }
    }).catch(handleError); // eslint-disable-line no-undef
  });

  document.addEventListener('accountsignupRequest', (e) => { // eslint-disable-line no-undef
    handleEvent(buildUrl(accountAddress.accountSignup), 'GET', e.detail, e.target).then(() => { // eslint-disable-line no-undef
      const element = e.target.querySelector('#accountSignupFormSubmit');
      if (element) {
        element.addEventListener('click', (innerEvent) => {
          innerEvent.preventDefault();
          if (formValidation(e.target, '#accountSignupFormEmail', '#accountSignupFormPassword') === true) { // eslint-disable-line no-undef
            const email = e.target.querySelector('#accountSignupFormEmail').value;
            const password = e.target.querySelector('#accountSignupFormPassword').value;
            handleEvent(buildUrl(accountAddress.accountSignup), 'POST', e.detail, e.target, { email, password }).catch(handleError); // eslint-disable-line no-undef
          }
        });
      }
    }).catch(handleError); // eslint-disable-line no-undef
  });
};

accountEvents().catch(handleError); // eslint-disable-line no-undef
