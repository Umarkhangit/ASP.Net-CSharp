$(function () {
    getAllEmployees(); // to call the function after the document is ready
})

const tBody = document.getElementById("empTable")

const getAllEmployees = async () => {
    try {
        const res = await axios.post("https://localhost:44392/Employees.asmx/SelectAllEmployees")
        if (res.status === 200) {
            let employees = [...res.data]
            tBody.innerHTML = generateRows(employees)
            //console.log("Employees fetched successfully:", res);
        } else {
            console.error("Failed to fetch employees:", res);
        }
    }
    catch (e) {
        console.error(e.response)
    }
}

const getEmployeeById = async () => {
    const id = document.getElementById("empId").value

    if (id) {
        try {
            const res = await axios.post("https://localhost:44392/Employees.asmx/SelectEmployee", { empId: id })
            if (res.status == 200) {
                const rawResponse = res.data;
                // Clean and parse the response
                const cleanedResponse = rawResponse.match(/^\{.*?\}(?=\{)/);
                const responseData = JSON.parse(cleanedResponse[0]);

                if (responseData.Status == "Success") {
                    console.log(responseData.data);
                    tBody.innerHTML = generateRows(responseData.data)
                } else {
                    alert(responseData.Description)
                }
            } else {
                console.error(res.data)
            }
        }
        catch (e) {
            console.error(e.response)
        }
    } else {
        alert("Please enter the Id")
    }
}

//let newEmp = {}
//const handleChange = (val) => {
//    newEmp = { ...newEmp, [val.name]: val.value }
//    console.log("emp", newEmp)
//}

let empid = document.getElementById("id").value
let empname = document.getElementById("name").value
let emprole = document.getElementById("role").value
let empsalary = document.getElementById("salary").value
let empmobile = document.getElementById("mobile").value

const insertEmployee = () => {
    if (!empname && !emprole && !empsalary && !empmobile) {
        alert("Pls fill all the fields")
    }
    else if (!empmobile.match(/^\d{10}$/)) {
        alert("Mobile number should be of 10 digits")
    } else {
        try {
            let newEmp = {
                empName: empname,
                empRole: emprole,
                empSalary: Number(empsalary),
                empMobile: empmobile
            }
            axios.post("https://localhost:44392/Employees.asmx/InsertEmployee", newEmp)
                .then(res => {
                    const rawResponse = res.data;
                    // Clean and parse the response
                    const cleanedResponse = rawResponse.match(/^\{.*?\}(?=\{)/);
                    const responseData = JSON.parse(cleanedResponse[0]);

                    if (responseData.Status == "Success") {
                        console.log(responseData)
                        alert(responseData.Description)
                        getAllEmployees();
                    } else {
                        alert(responseData.Description)
                    }
                })
                .catch(e => console.error(e))

            closeModel()
        } catch (e) {
            console.error(e.response)
        }
    }
}

const updateEmployee = () => {
    if (!empname && !emprole && !empsalary && !empmobile) {
        alert("Pls fill all the fields")
    }
    else if (!empmobile.match(/^\d{10}$/)) {
        alert("Mobile number should be of 10 digits")
    } else {
        let udatedEmp = {
            empId: empid,
            empName: empname,
            empRole: emprole,
            empSalary: Number(empsalary),
            empMobile: empmobile
        }
        axios.post("https://localhost:44392/Employees.asmx/UpdateEmployee", udatedEmp)
            .then(res => {
                const rawResponse = res.data;
                // Clean and parse the response
                const cleanedResponse = rawResponse.match(/^\{.*?\}(?=\{)/);
                const responseData = JSON.parse(cleanedResponse[0]);

                if (responseData.Status == "Success") {
                    alert(responseData.Description)
                    getAllEmployees();
                } else {
                    alert(responseData.Description)
                }
            })
            .catch(e => console.error(e))

        closeModel()
    }
}

const deleteEmployee = (id) => {
    axios.post("https://localhost:44392/Employees.asmx/DeleteEmployee", { empId: id })
        .then(res => {
            const rawResponse = res.data;
            // Clean and parse the response
            const cleanedResponse = rawResponse.match(/^\{.*?\}(?=\{)/);
            const responseData = JSON.parse(cleanedResponse[0]);

            if (responseData.Status == "Success") {
                alert(responseData.Description)
                getAllEmployees();
            } else {
                alert(responseData.Description)
            }
        })
        .catch(e => console.error(e))

    getAllEmployees();
}

const generateRows = (data) => {
    let rows = ``
    if (data != null) {
        data.forEach(val => {
            rows += `<tr>
                        <td>${val.EmpID}</td>
                        <td>${val.EmpName}</td>
                        <td>${val.EmpRole}</td>
                        <td>${val.EmpSalary}</td>
                        <td>${val.EmpMobile}</td>
                        <td style="font-size:20px"><i class="bi bi-pencil-square" style="color:cornflowerblue;cursor:pointer" onclick=update(${val.EmpID})></i> || 
                        <i class="bi bi-trash3" style="color:red;cursor:pointer" onclick=deleteEmployee(${val.EmpID})></i></td>
                    </tr>`
        })
        return rows
    } else {
        rows += `<tr><td colspan="5">No data found</td></tr>`
        return rows
    }
}

const clearIdSearch = () => {
    empid = ""
    getAllEmployees();
}

const update = (id) => {
    axios.post("https://localhost:44392/Employees.asmx/SelectAllEmployees")
        .then(res => {
            let filteredEmp = res.data.find(val => val.EmpID == id)
            console.log("filter", filteredEmp)
            openModal(filteredEmp)
        })
        .catch(e => console.error(e))
}


const openModal = (fEmp) => {
    console.log("femp", fEmp)
    let modal = ``
    if (fEmp) {
        modal = `<div id="mForm">
            <h3>Update</h3>
            <div class="mb-3 row">
            <label for="name" class="col-sm-2 col-form-label">Id</label>
            <div class="col-sm-10">
                <input type="text" class="form-control" value=${fEmp.EmpID} id = "id" name = "empId" required = "required" />
            </div>
            </div>
            <div class="mb-3 row">
                <label for="name" class="col-sm-2 col-form-label">Name</label>
                <div class="col-sm-10">
                    <input type="text" class="form-control" value=${fEmp.EmpName} id = "name" name = "empName" required = "required" />
                </div>
            </div>
             <div class="mb-3 row">
                <label for="role" class="col-sm-2 col-form-label">Role</label>
                <div class="col-sm-10">
                    <input type="text" class="form-control" value=${fEmp.EmpRole} id="role" name="empRole" required="required"/>
                </div>
             </div>
             <div class="mb-3 row">
                <label for="salary" class="col-sm-2 col-form-label">Salary</label>
                <div class="col-sm-10">
                    <input type="text" class="form-control" value=${fEmp.EmpSalary} id="salary" name="empSalary" required="required"/>
                </div>
             </div>
            <div class="mb-3 row">
                <label for="mobile" class="col-sm-2 col-form-label">Mobile</label>
                <div class="col-sm-10">
                    <input type="text" class="form-control" value=${fEmp.EmpMobile} id="mobile" name="empMobile" required="required"/>
                </div>
            </div>
            <div id="actions" class="d-flex justify-content-end">
              <button type="button" class="btn btn-primary me-2" onclick="updateEmployee()">Submit</button>
              <button type="button" class="btn btn-secondary" onclick="closeModel()">Cancel</button>
            </div>
            </div>`

        const mForm = document.getElementById("modalForm")
        mForm.innerHTML = modal
    } else {
        modal = `<div id="mForm">
                <h3>Add new</h3>
                <div class="mb-3 row">
                    <label for="name" class="col-sm-2 col-form-label">Name</label>
                    <div class="col-sm-10">
                        <input type="text" class="form-control" id = "name" name="empName" required = "required" />
                    </div>
                </div>
                 <div class="mb-3 row">
                    <label for="role" class="col-sm-2 col-form-label">Role</label>
                    <div class="col-sm-10">
                        <input type="text" class="form-control" id="role" name="empRole" required="required"/>
                    </div>
                 </div>
                 <div class="mb-3 row">
                    <label for="salary" class="col-sm-2 col-form-label">Salary</label>
                    <div class="col-sm-10">
                        <input type="text" class="form-control" id="salary" name="empSalary" required="required"/>
                    </div>
                 </div>
                <div class="mb-3 row">
                    <label for="mobile" class="col-sm-2 col-form-label">Mobile</label>
                    <div class="col-sm-10">
                        <input type="text" class="form-control" id="mobile" name="empMobile" required="required"/>
                    </div>
                </div>
                <div id="actions" class="d-flex justify-content-end">
                  <button type="button" class="btn btn-primary me-2" onclick="insertEmployee()">Submit</button>
                  <button type="button" class="btn btn-secondary" onclick="closeModel()">Cancel</button>
                </div>
            </div>`

        const mForm = document.getElementById("modalForm")
        mForm.innerHTML = modal
    }
    
}

const closeModel = () => {
    empname = ""
    emprole = ""
    empsalary = ""
    empmobile = ""

    const mForm = document.getElementById("modalForm")
    mForm.removeChild(mForm.firstElementChild)
}

//        let modal = `
//            <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
//              <div class="modal-dialog">
//                <div class="modal-content">
//                  <div class="modal-header">
//                    <h1 class="modal-title fs-5" id="exampleModalLabel">Add new employee</h1>
//                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" onclick="closeModel()"></button>
//                  </div>
//                  <div class="modal-body">
//                    <div class="mb-3 row">
//                        <label for="name" class="col-sm-2 col-form-label">Name</label>
//                        <div class="col-sm-10">
//                            <input type="text" class="form-control" id="name" name = "empname" onchange = "handleChange(this)" required = "required" />
//                        </div>
//                     </div>
//                     <div class="mb-3 row">
//                        <label for="role" class="col-sm-2 col-form-label">Role</label>
//                        <div class="col-sm-10">
//                            <input type="text" class="form-control" id="role" name="emprole" onchange="handleChange(this)" required="required"/>
//                        </div>
//                     </div>
//                     <div class="mb-3 row">
//                        <label for="salary" class="col-sm-2 col-form-label">Salary</label>
//                        <div class="col-sm-10">
//                            <input type="text" class="form-control" id="salary" name="empsalary" onchange="handleChange(this)" required="required"/>
//                        </div>
//                     </div>
//                    <div class="mb-3 row">
//                        <label for="mobile" class="col-sm-2 col-form-label">Mobile</label>
//                        <div class="col-sm-10">
//                            <input type="text" class="form-control" id="mobile" name="empmobile" onchange="handleChange(this)" required="required"/>
//                        </div>
//                    </div>
//                  </div>
//                  <div class="modal-footer">
//                    <button type="button" class="btn btn-primary" onclick="insertEmployee()" id="submitBtn">Submit</button>
//                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" onclick="closeModel()">Cancel</button>
//                  </div>
//                </div>
//              </div>
//            </div>`