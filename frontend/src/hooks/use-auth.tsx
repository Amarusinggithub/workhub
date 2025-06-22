import React, { createContext, useContext, useState, type PropsWithChildren } from 'react';
import type { AuthErrorCode, SharedData } from '../types';

type AuthContextType = {
	errors: AuthErrorCode[] | null;

	sharedData: SharedData | undefined;
	isAuthenticated: boolean;
	isCheckingAuth: boolean;
	isLoading: boolean;
	setLoading: React.Dispatch<React.SetStateAction<boolean>>;
	setError: React.Dispatch<React.SetStateAction<AuthErrorCode[] | null>>;
	setAuthenticated: React.Dispatch<React.SetStateAction<boolean>>;
	setIsCheckingAuth: React.Dispatch<React.SetStateAction<boolean>>;
	setSharedData: React.Dispatch<React.SetStateAction<SharedData | undefined>>;
	SignUp: (firstName: string, lastName: string, email: string, password: string, confirmPassword: string) => void;
};

type AuthProviderProps = PropsWithChildren;

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: AuthProviderProps) {
	const [isAuthenticated, setAuthenticated] = useState<boolean>(false);
	const [isCheckingAuth, setIsCheckingAuth] = useState<boolean>(false);
	const [sharedData, setSharedData] = useState<SharedData | undefined>();
	const [isLoading, setLoading] = useState<boolean>(false);
	const [errors, setError] = useState<AuthErrorCode[] | null>(null);

	function SignUp(firstName: string, lastName: string, email: string, password: string, confirmPassword: string) {
		try {
			setLoading(true);
			setError(null);
			const tempErrors: AuthErrorCode[] = [];

			if (!firstName.trim()) tempErrors.push('firstName:This field cannot be empty');
			if (!lastName.trim()) tempErrors.push('lastName:This field cannot be empty');
			if (!email.trim()) tempErrors.push('email:This field cannot be empty');
			if (!password.trim()) tempErrors.push('password:This field cannot be empty');
			if (!confirmPassword.trim()) tempErrors.push('confirmPassword:This field cannot be empty');

			if (password && confirmPassword && password !== confirmPassword) {
				tempErrors.push('confirmPassword:password and confirm password must match');
			}

			if (tempErrors.length > 0) {
				setError(tempErrors);
				return;
			}


		} catch (e) {
			console.log(e);
		} finally {
			setLoading(false);
		}
	}

	return (
		<AuthContext.Provider
			value={{
				errors,
				SignUp,
				setError,
				isLoading,
				setLoading,
				isAuthenticated,
				setAuthenticated,
				isCheckingAuth,
				setIsCheckingAuth,
				sharedData,
				setSharedData,
			}}
		>
			{children}
		</AuthContext.Provider>
	);
}

export default function useAuth() {
	const context = useContext(AuthContext);
	if (!context) {
		throw new Error('useAuth must be used within the context of AuthProvider');
	}
	return context;
}
