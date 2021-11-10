function apiCall(url, method, options) { // eslint-disable-line
  return new Promise((resolve, reject) => {
    // eslint-disable-next-line no-undef
    const xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = (event) => {
      if (event.target.readyState === 4) {
        if (event.target.status === 200) {
          let result;
          if (event.target.responseText) {
            result = JSON.parse(event.target.responseText);
          }

          resolve(result);
        } else {
          reject();
        }
      }
    };

    // eslint-disable-next-line no-undef
    const token = document.querySelector('meta[name="csrf-token"]').getAttribute('content');
    xhttp.open(method, url, true);
    xhttp.withCredentials = true;
    xhttp.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
    xhttp.setRequestHeader('CSRF-Token', token);
    // const data = Object.keys(options).map((key) => `${key}=${options[key]}`).join('&');
    // xhttp.send(data);
    xhttp.send();
  });
}
