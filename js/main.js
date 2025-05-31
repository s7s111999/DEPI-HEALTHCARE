 
document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('loginForm');
    if (form) {
      form.addEventListener('submit', function (e) {
        e.preventDefault();
        const email = document.getElementById('email').value;
        const password = document.getElementById('password').value;
        fetch('/api/auth/login', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({ email, password })
        })
        .then(res => res.json())
        .then(data => {
          if (data.token) {
            localStorage.setItem('token', data.token);
            window.location.href = 'dashboard.html';
          } else {
            alert('Login failed');
          }
        });
      });
    }
  });
  