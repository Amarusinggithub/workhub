import React, { createContext, useContext, useState, type PropsWithChildren } from 'react';
import type { SharedData } from '../types';

type AuthContextType = {
	errors: string;

	sharedData: SharedData | undefined;
	isAuthenticated: boolean;
	isCheckingAuth: boolean;
	isLoading: boolean;
	setLoading: React.Dispatch<React.SetStateAction<boolean>>;
	setError: React.Dispatch<React.SetStateAction<string>>;
	setAuthenticated: React.Dispatch<React.SetStateAction<boolean>>;
	setIsCheckingAuth: React.Dispatch<React.SetStateAction<boolean>>;
	setSharedData: React.Dispatch<React.SetStateAction<SharedData | undefined>>;
};

type AuthProviderProps = PropsWithChildren;

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: AuthProviderProps) {
	const [isAuthenticated, setAuthenticated] = useState<boolean>(false);
	const [isCheckingAuth, setIsCheckingAuth] = useState<boolean>(false);
	const [sharedData, setSharedData] = useState<SharedData | undefined>();
	const [isLoading, setLoading] = useState<boolean>(false);
	const [errors, setError] = useState<string>('');

	return (
		<AuthContext.Provider
			value={{
				errors,
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
