window.onload = function () {
    // split the window location to get the id of the teacher
    const hrefArray = window.location.pathname.split('/');

    // declare all the variables here
    const confirmDelete = document.getElementById('confirmDelete');

    // use this function to delete a teacher
    function onDeleteTeacher() {

        // extract the last element of the array as the path is
        // .../Confirm/{id}
        const teacherId = Number(hrefArray[hrefArray.length - 1]);

        // create a new xhr instance
        const xhr = new XMLHttpRequest();

        // api url
        const apiUrl = `http://localhost:50860/api/deleteTeacher/${teacherId}`;

        // open the connection
        xhr.open('POST', apiUrl, true);

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

        // send the request
        xhr.send();
    }

    // map all the event listners
    confirmDelete.addEventListener('click', onDeleteTeacher);

}