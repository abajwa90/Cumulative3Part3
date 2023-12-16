window.onload = function () {

    // declare all the variables here
    const firstName = document.getElementById('firstName');
    const validFirstName = document.getElementById('validFirstName');
    const lastName = document.getElementById('lastName');
    const validLasttName = document.getElementById('validLasttName');
    const employeeNumber = document.getElementById('employeeNumber');
    const validEmployeeNumber = document.getElementById('validEmployeeNumber');
    const salary = document.getElementById('salary')
    const validSalary = document.getElementById('validSalary');

    // declare all the regular expression required for adding a teacher
    const nameRegexp = /^[A-Z a-z]+$/;
    const employeeRegexp = /^[A-Za-z]\d{3}$/;
    const numberRegexp = /^[0-9]+$/
    const formData = document.getElementById('addTeacherForm');

    // function to add a new teacher
    function onAddTeacher(e) {
        e.preventDefault();

        // check if the firstname is filled or not
        if (formData['firstName']?.value === "") {
            firstName?.classList?.add('is-invalid');
            validFirstName.classList.remove('d-none');
            validFirstName.innerHTML = "First name cannot be empty!";
            return false;
        }
        firstName?.classList?.remove('is-invalid');
        validFirstName.classList.add('d-none');
        validFirstName.innerHTML = "";

        // check if firstname matches with name regexp
        if (!nameRegexp.test(formData['firstName']?.value) ) {
            firstName?.classList?.add('is-invalid');
            validFirstName.classList.remove('d-none');
            validFirstName.innerHTML = "Special Characters not allowed!";
            return false;
        }

        firstName?.classList?.remove('is-invalid');
        validFirstName.classList.add('d-none');
        validFirstName.innerHTML = "";

        // check if the firstname has min length greater then 2
        if (formData['firstName']?.value?.length <= 2) {
            firstName?.classList?.add('is-invalid');
            validFirstName.classList.remove('d-none');
            validFirstName.innerHTML = "First name cannot be of two characters!";
            return false;
        }

        firstName?.classList?.remove('is-invalid');
        validFirstName.classList.add('d-none');
        validFirstName.innerHTML = "";


        // check if the lastname is filled or not
        if (formData['lastName']?.value === "") {
            lastName?.classList?.add('is-invalid');
            validLasttName.classList.remove('d-none');
            validLasttName.innerHTML = "Last name cannot be empty!";
            return false;
        }
        lastName?.classList?.remove('is-invalid');
        validLasttName.classList.add('d-none');
        validLasttName.innerHTML = "";

        // check if lastname matches with name regexp
        if (!nameRegexp.test(formData['lastName']?.value)) {
            lastName?.classList?.add('is-invalid');
            validLasttName.classList.remove('d-none');
            validLasttName.innerHTML = "Special Characters not allowed!";
            return false;
        }

        lastName?.classList?.remove('is-invalid');
        validLasttName.classList.add('d-none');
        validLasttName.innerHTML = "";

        // check if the lastname has min length greater then 2
        if (formData['lastName']?.value?.length <= 2) {
            lastName?.classList?.add('is-invalid');
            validLasttName.classList.remove('d-none');
            validLasttName.innerHTML = "Last name cannot be of two characters!";
            return false;
        }

        lastName?.classList?.remove('is-invalid');
        validLasttName.classList.add('d-none');
        validLasttName.innerHTML = "";

        // check if the employee number is filled or not
        if (formData['employeeNumber']?.value === "") {
            employeeNumber?.classList?.add('is-invalid');
            validEmployeeNumber.classList.remove('d-none');
            validEmployeeNumber.innerHTML = "Employee number cannot be empty!";
            return false;
        }
        employeeNumber?.classList?.remove('is-invalid');
        validEmployeeNumber.classList.add('d-none');
        validEmployeeNumber.innerHTML = "";

        // check if employee number matches with the regexp
        if (!employeeRegexp.test(formData['employeeNumber']?.value)) {
            employeeNumber?.classList?.add('is-invalid');
            validEmployeeNumber.classList.remove('d-none');
            validEmployeeNumber.innerHTML = "Please match the format of the employee number!";
            return false;
        }

        employeeNumber?.classList?.remove('is-invalid');
        validEmployeeNumber.classList.add('d-none');
        validEmployeeNumber.innerHTML = "";

      
        // check if the salary is entered or not
        if (formData['salary']?.value === "") {
            salary?.classList?.add('is-invalid');
            validSalary.classList.remove('d-none');
            validSalary.innerHTML = "Salary cannot be empty!";
            return false;
        }
        salary?.classList?.remove('is-invalid');
        validSalary.classList.add('d-none');
        validSalary.innerHTML = "";
        
        // check if salary matches with the regexp
        if (!numberRegexp.test(formData['salary']?.value)) {
            salary?.classList?.add('is-invalid');
            validSalary.classList.remove('d-none');
            validSalary.innerHTML = "Special Characters not allowed!";
            return false;
        }

        salary?.classList?.remove('is-invalid');
        validSalary.classList.add('d-none');
        validSalary.innerHTML = "";

        // check if salary is greater than 0
        if (formData['salary']?.value <=0 ) {
            salary?.classList?.add('is-invalid');
            validSalary.classList.remove('d-none');
            validSalary.innerHTML = "Salary should be greater than 0!";
            return false;
        }

        salary?.classList?.remove('is-invalid');
        validSalary.classList.add('d-none');
        validSalary.innerHTML = "";

        // create the new teacher instance
        const newTeacherData = {
            firstName: formData['firstName']?.value,
            lastName: formData['lastName']?.value,
            employeeNumber: formData['employeeNumber']?.value,
            salary: formData['salary']?.value,
        }

        // create a new xhr instance
        const xhr = new XMLHttpRequest();
        // api url
        const url = "http://localhost:50860/api/addTeacher";

        // open the conenction
        xhr.open('POST', url, true);
        xhr.setRequestHeader('content-type','application/json')
        
        // check for on state change
        xhr.onreadystatechange = function () {
            // check if the request state and ready state
            // stauts 204 because we are not sending any response back
            if (xhr.readyState == 4 && xhr.status == 204) {

                // redirect back to the teachers list page
                window.location.href = "http://localhost:50860/Teachers/List";
            }
            else {
                // extract the error response and convert it to json.
                const errorResponse = JSON.parse(xhr.response);

                // redirect to error page
                window.location.href = "http://localhost:50860/Teachers/error?message=" + errorResponse?.InnerException?.ExceptionMessage;
            }

        }

        // send the api call.
        xhr.send(JSON.stringify(newTeacherData));
    }

    // map all the event listners
    formData.addEventListener('submit', onAddTeacher)
}