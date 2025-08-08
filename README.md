# Supervisors Control App

A full-stack web application for managing supervisors' daily reports, including detailed activities, validations, and role-based access.

## 🚀 Stack
**Frontend**  
- Angular 17 (Standalone Components)  
- TypeScript  
- Bootstrap + custom styles  
- JWT authentication  
- Role-based UI rendering (admin, manager, supervisor)  

**Backend**  
- .NET 6 Web API  
- Entity Framework Core (Pomelo MariaDB Provider)  
- JWT authentication & authorization  
- RESTful endpoints with Swagger documentation  
- Validation with DataAnnotations  
- Atomic persistence with EF Core transactions  

**Database**  
- MariaDB

---

## 📋 Features

- **Authentication & Authorization**: JWT-based login, role-based permissions.
- **Daily Reports**: Create, edit, and complete reports.
- **Report Details**: Add multiple activities with objectives, vigilators, and time tracking.
- **Validations**:
  - End time > Start time
  - Final kilometers ≥ Initial kilometers
  - Activity hours sum up to total hours
- **Progressive Saving**: Partial updates without locking the workflow.
- **Role Restrictions**:
  - Only Admin/Manager can annul/restore reports or confirm payments.

---

## 🛠 Installation

### **Backend**
1. Clone repository
  
   git clone https://github.com/MDelgado23/SupervisoresApp/
