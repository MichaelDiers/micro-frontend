const accountView = async () => {
  document.addEventListener('DOMContentLoaded', () => { // eslint-disable-line no-undef
    apiCall(accountAddress.accountIndex, 'GET').then((accontIndexView) => { // eslint-disable-line no-undef
      const callback = async () => {
        const element = document.getElementById('account_load_logon'); // eslint-disable-line no-undef
        if (element) {
          element.addEventListener('click', () => {
            apiCall(accountAddress.accountLogon, 'GET').then((accountLogonView) => { // eslint-disable-line no-undef
              document.dispatchEvent(new CustomEvent('mainViewRequest', { // eslint-disable-line no-undef
                bubbles: true,
                cancelable: true,
                detail: {
                  view: accountLogonView,
                },
              }));
            }).catch(handleError); // eslint-disable-line no-undef
          });
        }
      };

      document.dispatchEvent(new CustomEvent('accountIndexViewRequest', { // eslint-disable-line no-undef
        bubbles: true,
        cancelable: true,
        detail: {
          view: accontIndexView,
          callback,
        },
      }));
    }).catch(handleError); // eslint-disable-line no-undef
  });
};

accountView().catch(handleError); // eslint-disable-line no-undef
