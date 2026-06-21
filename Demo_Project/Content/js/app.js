const sign_in_btn = document.querySelector("#sign-in-btn");
const sign_up_btn = document.querySelector("#sign-up-btn");
const container = document.querySelector(".container");

sign_up_btn.addEventListener('click', () => {
    container.classList.add("sign-up-mode");
});

sign_in_btn.addEventListener('click', () => {
    container.classList.remove("sign-up-mode");
});


 const form    = document.getElementById('loginForm');
  const msgBox  = document.getElementById('successMsg');

  form.addEventListener('submit', async (e) => {
    e.preventDefault();                 

                                            

    If you need to POST to a server, do it here.
       Show the message only when the server responds OK.
    
    const resp = await fetch('/login', {
      method: 'POST',
      body: new FormData(form)
    });
    if (!resp.ok) {
      // handle login errors here (e.g., shake form, show “Invalid credentials”)
      return;
    }
  



    // show success message
    msgBox.classList.remove('hidden');

    // Optional UX niceties
    form.reset();                        // clear inputs
    setTimeout(() => {
      msgBox.classList.add('hidden');    // hide after 3 s
      // window.location.href = '/dashboard';  // OR redirect
    }, 3000);
  });


//    It is normal sucess message



const form = document.getElementById('signupForm');
const notice = document.getElementById('successMsg');

form.addEventListener('submit', (e) => {
    e.preventDefault();           // stop full‑page reload
    if (!form.checkValidity()) {  // built‑in HTML validation
        form.reportValidity();      // show native validation popups
        return;
    }

    notice.classList.remove('hidden');  // show the green line
    form.reset();                       // clear inputs (optional)

    // auto‑hide after 3 seconds (optional)
    setTimeout(() => notice.classList.add('hidden'), 3000);
});


document.addEventListener('click', e => {
    if (e.target.classList.contains('toggle-password')) {
        const pwdInput = e.target.previousElementSibling;      // <input>
        pwdInput.type = pwdInput.type === 'password' ? 'text' : 'password';

        // icon change (eye ↔ eye‑slash)
        e.target.classList.toggle('fa-eye');
        e.target.classList.toggle('fa-eye-slash');
    }
});


