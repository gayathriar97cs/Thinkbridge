function userValid()
 {
    var Name, pass;

    Name = document.getElementById("txtUserName").value;
    pass = document.getElementById("txtPassword").value;
    
    if (Name == '' && pass == '') 
    {

        alert( "Enter User Name");
        return false;

    }
    if (Name == '' || Name == 'Username')
     {
        alert( "Please Enter User Name");
        return false;
    }
    
    if (pass == '' || pass == 'Password')
    {
        alert( "Please Enter Password");

        return false;
    }  
 
    return true;
}


