import { LucideIcon } from 'lucide-react';


type CreateUser = {
	FirtName: string;
	LastName: string;
	ProfilePicture?: string;
	HeaderImage?: string;
	JobTItle?: string;
	Email: string;
	password?: string;
	Organization?: string;
	Location?: string;
	UpdatedAt: Date;
	CreatedAt: Date;
	LastLoggedIn: Date;
	IsActive: boolean;

};

interface User extends CreateUser {
	id: number;
    name: string;
    EmailVerifiedAt: Date | null;
    [key: string]: unknown;

}

type OAuthAccount = {};

type WorkSpace = {};

type Project = {};

type Category = {};

type Plan = {};

type Notification = {};

type Task = {};

type Project = {};

type Resource = {};

type Role = {};

type Offer = {};

type UserGroUp = {};




export interface Auth {
    user: User;
}

export interface BreadcrumbItem {
    title: string;
    href: string;
}

export interface NavGroup {
    title: string;
    items: NavItem[];
}

export interface NavItem {
    title: string;
    href: string;
    icon?: LucideIcon | null;
    isActive?: boolean;
}

export interface SharedData {
    name: string;
    quote: { message: string; author: string };
    auth: Auth;
    sidebarOpen: boolean;
    [key: string]: unknown;
}



