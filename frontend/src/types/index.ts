 type CreateUser ={
    FirtName:string;
 LastName :string;
 ProfilePicture?:string;
 HeaderImage?:string;
  JobTItle?:string;
Email:string;
password?:string
   Organization?:string;
  Location?:string;
  UpdatedAt:Date;
  CreatedAt:Date;
  LastLoggedIn:Date;
 IsActive:boolean;
 }

 interface User extends CreateUser{
    id:number;

 }

 type OAuthAccount={

 }

 type WorkSpace={

 }

 type Project={

 }

 type Category={

 }

 type Plan={

 }

 type Notification={

 }

 type Task={

 }

 type Project={

 }

 type Resource={

 }

 type Role={

 }

 type Offer={

 }

 type UserGroUp={

 }






