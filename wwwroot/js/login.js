document.addEventListener('DOMContentLoaded', function () {
    const authToken = localStorage.getItem('authToken');
    const currentPage = window.location.pathname;

    if (!authToken && currentPage !== '/Auth/Login') {
        window.location.href = '/Auth/Login';
    } else if (authToken && currentPage === '/Auth/Login') {
        window.location.href = '/folders';
    }

    const loginForm = document.getElementById('loginForm');
    const emailInput = document.getElementById('email');
    const passwordInput = document.getElementById('password');

    loginForm.addEventListener('submit', function (event) {
        event.preventDefault();

        const email = emailInput.value.trim();
        const password = passwordInput.value.trim();

        if (email && password) {
            login(email, password);
        }
    });

    function login(email, password) {
        fetch('/api/auth/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Email: email,
                Password: password
            })
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            return response.json();
        })
        .then(data => {
            if (data.token) {
                localStorage.setItem('authToken', data.token);
                
                try {
                    if (typeof jwt_decode !== 'undefined') {
                        const decodedToken = jwt_decode(data.token);
                        const userId = decodedToken.nameid;

                        if (userId) {
                            localStorage.setItem('userId', userId);
                        } else {
                            console.error('No se pudo encontrar el userId en el token decodificado');
                        }
                    } else {
                        console.error('jwt_decode no está definido');
                    }
                } catch (error) {
                    console.error('Error al decodificar el token:', error);
                }

                window.location.href = '/folders';
            } else {
                console.error('Login failed:', data.message);
                localStorage.removeItem('authToken');
                alert('Error en el inicio de sesión: ' + (data.message || 'Error desconocido'));
            }
        })
        .catch(error => {
            console.error('Error en el inicio de sesión:', error);
            alert('Error en el inicio de sesión: ' + error.message);
        });
    }
});
