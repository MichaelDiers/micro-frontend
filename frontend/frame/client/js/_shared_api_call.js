function apiCall(url, method, options) { // eslint-disable-line
  return new Promise((resolve, reject) => {
    // eslint-disable-next-line no-undef
    const xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = (event) => {
      if (event.target.readyState === 4) {
        if (event.target.status === 200) {
          let result;
          if (event.target.responseText) {
            if (xhttp.getResponseHeader('content-type').startsWith('application/json')) {
              result = JSON.parse(event.target.responseText);
            } else {
              result = event.target.responseText;
            }
          }

          resolve(result);
        } else {
          reject();
        }
      }
    };

    xhttp.open(method, url, true); // eslint-disable-line no-undef
    xhttp.withCredentials = true;
    xhttp.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
    const csrfToken = document.querySelector('meta[name="csrf-token"]'); // eslint-disable-line no-undef
    if (csrfToken) {
      const token = csrfToken.getAttribute('content');
      if (token) {
        xhttp.setRequestHeader('CSRF-Token', token);
      }
    }

    // const data = Object.keys(options).map((key) => `${key}=${options[key]}`).join('&');
    // xhttp.send(data);
    xhttp.send();
  });
}
