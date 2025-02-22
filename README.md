
<div class="relative z-10 flex max-w-full h-fit" id="prosemirror-editor-container" style="padding-bottom: max(3rem, 24vh);"><div class="_main_5jn6z_1 z-10 markdown prose contain-inline-size dark:prose-invert focus:outline-none bg-transparent ProseMirror" contenteditable="true" style="width: 580px;" translate="no"><h1><span>Employee Management Web API (Clean Code)</span></h1><h2><span>📌 Overview</span></h2><p><span>This project is a </span><span><strong>cleanly structured Web API</strong></span><span> built using </span><span><strong>.NET Core</strong></span><span>, implementing </span><span><strong>CQRS</strong></span><span> with </span><span><strong>MediatR</strong></span><span>, </span><span><strong>Entity Framework Core</strong></span><span>, and </span><span><strong>Repository Pattern</strong></span><span> for managing employee records. It provides RESTful endpoints for CRUD operations such as adding, retrieving, updating, and deleting employee details.</span></p><h2><span>🚀 Technologies Used</span></h2><ul data-spread="false"><li><p><span><strong>.NET Core 8</strong></span><span> - Backend framework</span></p></li><li><p><span><strong>Entity Framework Core</strong></span><span> - ORM for database interactions</span></p></li><li><p><span><strong>MediatR</strong></span><span> - Implements CQRS pattern</span></p></li><li><p><span><strong>Microsoft.Extensions.Logging</strong></span><span> - Logging system</span></p></li><li><p><span><strong>NUnit</strong></span><span> - Unit testing</span></p></li><li><p><span><strong>Serilog</strong></span><span> - Structured logging</span></p></li><li><p><span><strong>Swagger</strong></span><span> - API documentation</span></p></li><li><p><span><strong>MySQL</strong></span><span> - Database</span></p></li></ul><h2><span>📂 Project Structure</span></h2><div class="cm-editor ͼ1 ͼ3 ͼ4 ͼ1o ͼ7p" data-is-code-block-view="true" contenteditable="false"><div class="cm-announced" aria-live="polite"></div><div tabindex="-1" class="cm-scroller"><div spellcheck="false" autocorrect="off" autocapitalize="off" translate="no" contenteditable="true" class="cm-content" role="textbox" aria-multiline="true" style="tab-size: 4;"><div class="cm-line">EmployeeManagement.WebAPI</div><div class="cm-line">│── EmployeeManagement.API          # API layer (Controllers, Middlewares)</div><div class="cm-line">│── EmployeeManagement.Application  # Business logic and MediatR handlers</div><div class="cm-line">│── EmployeeManagement.Domain       # Entities and domain models</div><div class="cm-line">│── EmployeeManagement.Persistence  # Database context &amp; repository pattern</div><div class="cm-line">│── EmployeeManagement.Tests        # Unit and integration tests</div></div><div class="cm-layer cm-layer-above cm-cursorLayer" aria-hidden="true" style="z-index: 150; animation-duration: 1200ms; animation-name: cm-blink2;"><div class="cm-cursor cm-cursor-primary" style="left: 181.869px; top: 94.5086px; height: 18.6741px;"></div></div><div class="cm-layer cm-selectionLayer" aria-hidden="true" style="z-index: -2;"></div></div></div><h2><span>🔧 Setup &amp; Installation</span></h2><ol data-spread="true" start="1"><li><p><span><strong>Clone the repository:</strong></span></p><div class="cm-editor ͼ1 ͼ3 ͼ4 ͼ1o ͼ7q" data-is-code-block-view="true" contenteditable="false"><div class="cm-announced" aria-live="polite"></div><div tabindex="-1" class="cm-scroller"><div spellcheck="false" autocorrect="off" autocapitalize="off" translate="no" contenteditable="true" class="cm-content" role="textbox" aria-multiline="true" style="tab-size: 4;"><div class="cm-line"><code>git clone https://github.com/sumraimtiyaz/Employee-Management-Web-API-Clean-Code.git</code></div><div class="cm-line"><code>cd Employee-Management-Web-API-Clean-Code</code></div></div><div class="cm-layer cm-layer-above cm-cursorLayer" aria-hidden="true" style="z-index: 150; animation-duration: 1200ms;"><div class="cm-cursor cm-cursor-primary" style="left: 5.99609px; top: 4.93115px; height: 18.6741px;"></div></div><div class="cm-layer cm-selectionLayer" aria-hidden="true" style="z-index: -2;"></div></div></div></li><li><p><span><strong>Configure database connection in </strong></span><code><span><strong>appsettings.json</strong></span></code><span>:</span></p><div class="cm-editor ͼ1 ͼ3 ͼ4 ͼ1o ͼ7r" data-is-code-block-view="true" contenteditable="false"><div class="cm-announced" aria-live="polite"></div><div tabindex="-1" class="cm-scroller"><div spellcheck="false" autocorrect="off" autocapitalize="off" translate="no" contenteditable="true" class="cm-content" role="textbox" aria-multiline="true" data-language="json" style="tab-size: 4;"><div class="cm-line"><code><span class="ͼ1y ͼe">"ConnectionStrings"</span>: <span class="ͼ1w">{</span></div><div class="cm-line">  <span class="ͼ1r">"DefaultConnection"</span>: <span class="ͼ1y ͼe">"Server=YOUR_SERVER;Database=employee_db;User Id=YOUR_USER;Password=YOUR_PASSWORD;"</span></div><div class="cm-line"><span class="ͼ1w">}</span></div></code></div><div class="cm-layer cm-layer-above cm-cursorLayer" aria-hidden="true" style="z-index: 150; animation-duration: 1200ms;"><div class="cm-cursor cm-cursor-primary" style="left: 5.99609px; top: 4.93115px; height: 18.6741px;"></div></div><div class="cm-layer cm-selectionLayer" aria-hidden="true" style="z-index: -2;"></div></div></div></li><li><p><span><strong>Apply migrations</strong></span><span>:</span></p><div class="cm-editor ͼ1 ͼ3 ͼ4 ͼ1o ͼ7s" data-is-code-block-view="true" contenteditable="false"><div class="cm-announced" aria-live="polite"></div><div tabindex="-1" class="cm-scroller"><div spellcheck="false" autocorrect="off" autocapitalize="off" translate="no" contenteditable="true" class="cm-content" role="textbox" aria-multiline="true" style="tab-size: 4;"><div class="cm-line"><code>dotnet ef migrations add InitialMigration</code></div><div class="cm-line"><code>dotnet ef database update</code></div></div><div class="cm-layer cm-layer-above cm-cursorLayer" aria-hidden="true" style="z-index: 150; animation-duration: 1200ms;"><div class="cm-cursor cm-cursor-primary" style="left: 5.99609px; top: 4.93115px; height: 18.6741px;"></div></div><div class="cm-layer cm-selectionLayer" aria-hidden="true" style="z-index: -2;"></div></div></div></li><li><p><span><strong>Run the API</strong></span><span>:</span></p><div class="cm-editor ͼ1 ͼ3 ͼ4 ͼ1o ͼ7t" data-is-code-block-view="true" contenteditable="false"><div class="cm-announced" aria-live="polite"></div><div tabindex="-1" class="cm-scroller"><div spellcheck="false" autocorrect="off" autocapitalize="off" translate="no" contenteditable="true" class="cm-content" role="textbox" aria-multiline="true" style="tab-size: 4;"><div class="cm-line"><code>dotnet run --project EmployeeManagement.API</code></div></div><div class="cm-layer cm-layer-above cm-cursorLayer" aria-hidden="true" style="z-index: 150; animation-duration: 1200ms;"><div class="cm-cursor cm-cursor-primary" style="left: 5.99609px; top: 4.93115px; height: 18.6741px;"></div></div><div class="cm-layer cm-selectionLayer" aria-hidden="true" style="z-index: -2;"></div></div></div></li><li><p><span><strong>Access Swagger UI</strong></span><span>:</span></p><ul data-spread="false"><li><p><span>Open: </span><code><span>http://localhost:5017/swagger</span></code></p></li></ul></li></ol>
  <h2>🛢 <strong data-start="160" data-end="176">MySQL Database</strong></h2>
<p><span>This document provides instructions for setting up and using the </span><code><span>employee_db</span></code><span> database, including the schema, stored procedures, and sample data.</span></p>
  <p><span>The script creates a new database named </span><code><span>employee_db</span></code><span> if it does not already exist.</span></p>
<code><span>CREATE DATABASE IF NOT EXISTS employee_db;</span></code><br>
  <code><span>USE employee_db;</span></code>
<h2><span>Employees Table</span></h2>
  <p><span>The </span><code><span>employees</span></code><span> table is created with the following columns:</span></p>
  <ul data-spread="false"><li><p><code><span>id</span></code><span> (Primary Key, Auto-incremented)</span></p></li><li><p><code><span>name</span></code><span> (Employee Name, Required)</span></p></li><li><p><code><span>email</span></code><span> (Employee Email, Unique, Required)</span></p></li><li><p><code><span>department</span></code><span> (Department Name)</span></p></li><li><p><code><span>salary</span></code><span> (Employee Salary, Decimal)</span></p></li><li><p><code><span>created_at</span></code><span> (Timestamp, Default: Current Time)</span></p></li></ul>
    <code><span>CREATE TABLE IF NOT EXISTS employees (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    department VARCHAR(100),
    salary DECIMAL(10,2),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);</span></code>
<h2><span>Stored Procedures</span></h2>
<h3><span>GetEmployees (Pagination Support)</span></h3>
<p><span>Retrieves a paginated list of employees using </span><code><span>pageIndex</span></code><span> and </span><code><span>rowsPerPage</span></code><span> parameters.</span></p>
<code><span>CREATE PROCEDURE GetEmployees(
    IN p_PageIndex INT,  -- 1-based page index
    IN p_RowsPerPage INT -- Number of records per page
)
BEGIN
    DECLARE v_Offset INT;
    SET v_Offset = (p_PageIndex - 1) * p_RowsPerPage;
    SELECT * FROM employees
    LIMIT p_RowsPerPage OFFSET v_Offset;
END;</span></code>
<h3><span>InsertEmployee</span></h3>
<p><span>Inserts a new employee record.</span></p>
    <code><span>CREATE PROCEDURE InsertEmployee(
    IN _name VARCHAR(100),
    IN _email VARCHAR(100),
    IN _department VARCHAR(100),
    IN _salary DECIMAL(10,2)
)
BEGIN
    INSERT INTO employees (name, email, department, salary)
    VALUES (_name, _email, _department, _salary);
END;</span></code>
<h3><span>UpdateEmployee</span></h3>
<p><span>Updates an existing employee record.</span></p>
    <code><span>CREATE PROCEDURE UpdateEmployee(
    IN _id INT,
    IN _name VARCHAR(100),
    IN _email VARCHAR(100),
    IN _department VARCHAR(100),
    IN _salary DECIMAL(10,2)
)
BEGIN
    UPDATE employees
    SET name = _name,
        email = _email,
        department = _department,
        salary = _salary
    WHERE id = _id;
END;</span></code>
<h3><span>DeleteEmployee</span></h3>
<p><span>Deletes an employee record by ID.</span></p>
<code><span>CREATE PROCEDURE DeleteEmployee(IN _id INT)
BEGIN
    DELETE FROM employees WHERE id = _id;
END;</span></code>    
<h2><span>Sample Data</span></h2>
    <p><span>The following sample employee records are inserted into the </span><code><span>employees</span></code><span> table:</span></p>
    <code><span>INSERT INTO employees (name, email, department, salary) VALUES
('John Doe', 'john.doe@example.com', 'Engineering', 75000),
('Hank Anderson', 'hank.anderson@example.com', 'Engineering', 95000);</span></code>
<h2><span>Usage</span></h2>
<h3><span>Retrieving Employees with Pagination</span></h3>
<p><span>To retrieve employees using pagination:</span></p>
<code><span>CALL GetEmployees(1, 5); -- Retrieves the first page with 5 employees per page</span></code>
<h3><span>Inserting an Employee</span></h3>
<code><span>CALL InsertEmployee('New Employee', 'new.email@example.com', 'HR', 65000);</span></code>
<h3><span>Updating an Employee</span></h3>
<code><span>CALL UpdateEmployee(1, 'Updated Name', 'updated.email@example.com', 'Finance', 85000);</span></code>
<h3><span>Deleting an Employee</span></h3>
<code><span>CALL DeleteEmployee(1);</span></code>
  <h2><span>Notes</span></h2> 
    <ul data-spread="false"><li><p><span>The pagination starts at page index </span><code><span>1</span></code><span>.</span></p></li><li><p><span>Ensure that unique emails are used to avoid conflicts.</span></p></li><li><p><span>Always back up data before executing delete operations.</span></p></li></ul>
  <h2><span>🔗 API Endpoints</span></h2><table><tbody><tr><th><span>Method</span></th><th><span>Endpoint</span></th><th><span>Description</span></th></tr><tr><td><span>GET</span></td><td><code><span>/api/Employees/GetAllEmployees</span></code></td><td><span>Get list of employees with pagination</span></td></tr><tr><td><span>GET</span></td><td><code><span>/api/Employees/GetEmployeeById{employeeId}</span></code></td><td><span>Get an employee by ID</span></td></tr><tr><td><span>POST</span></td><td><code><span>/api/Employees/CreateEmployee</span></code></td><td><span>Add new employee</span></td></tr><tr><td><span>PUT</span></td><td><code><span>/api/Employees/UpdateEmployee{employeeId}</span></code></td><td><span>Update employee details</span></td></tr><tr><td><span>DELETE</span></td><td><code><span>/api/Employees/DeleteEmployee{employeeId}</span></code></td><td><span>Delete an employee</span></td></tr></tbody></table><h2><span>📌 Contributing</span></h2><ol data-spread="false" start="1"><li><p><span><strong>Fork the repository</strong></span><span>.</span></p></li><li><p><span><strong>Create a new branch</strong></span><span>: </span><code><span>git checkout -b feature-new</span></code><span>.</span></p></li><li><p><span><strong>Make changes and commit</strong></span><span>: </span><code><span>git commit -m 'Added new feature'</span></code><span>.</span></p></li><li><p><span><strong>Push the branch</strong></span><span>: </span><code><span>git push origin feature-new</span></code><span>.</span></p></li><li><p><span><strong>Open a pull request</strong></span><span>.</span></p></li></ol><h2><span>☕ Support</span></h2><div class="pointer-events-none relative flex h-full flex-shrink-0 z-20 basis-0" style="width: 0px; opacity: 1; will-change: auto;"><div class="pointer-events-auto absolute bottom-0 left-0 top-0 w-0 overflow-visible pl-2"><p><span>If you find this project helpful and would like to support my work, you can </span><span><strong>buy me a coffee</strong></span><span>!</span></p> <a href="https://www.buymeacoffee.com/newarena7w" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/v2/default-red.png" alt="Buy Me A Coffee" style="height: 60px !important;width: 217px !important;" ></a></div></div></div>
