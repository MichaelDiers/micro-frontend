const accountEvents = async () => {
  const handleEvent = async (url, method, localUrl, targetElement, body) => {
    const target = targetElement;
    return apiCall(url, method, body).then((view) => { // eslint-disable-line no-undef
      target.innerHTML = view.replace(/__LOCAL__URL__/, localUrl);
    }).catch(handleError); // eslint-disable-line no-undef
  };

  document.addEventListener('accountHeaderLinkRequest', (e) => { // eslint-disable-line no-undef
    handleEvent(accountAddress.accountIndex, 'GET', e.detail, e.target).catch(handleError); // eslint-disable-line no-undef
  });

  document.addEventListener('accountlogonRequest', (e) => { // eslint-disable-line no-undef
    handleEvent(accountAddress.accountLogon, 'GET', e.detail, e.target).then(() => { // eslint-disable-line no-undef
      const element = e.target.querySelector('#accountLogonFormSubmit');
      if (element) {
        element.addEventListener('click', (innerEvent) => {
          innerEvent.preventDefault();
          const email = e.target.querySelector('#accountLogonFormEmail')?.value;
          const password = e.target.querySelector('#accountLogonFormPassword')?.value;
          handleEvent(accountAddress.accountLogon, 'POST', e.detail, e.target, { email, password }).catch(handleError); // eslint-disable-line no-undef
        });
      }
    }).catch(handleError); // eslint-disable-line no-undef
  });

  document.addEventListener('accountFeVersion', (e) => { // eslint-disable-line no-undef
    handleEvent(accountAddress.accountVersion, 'GET', e.detail, e.target).catch(handleError); // eslint-disable-line no-undef
  });
};

accountEvents().catch(handleError); // eslint-disable-line no-undef
