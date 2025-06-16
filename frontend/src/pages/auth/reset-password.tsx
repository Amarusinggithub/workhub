import { Button } from ' ../../components/ui/button';
import { LoaderCircle } from 'lucide-react';
import { FormEventHandler, useState } from 'react';
import InputError from '../../components/input-error';
import { Input } from '../../components/ui/input';
import { Label } from '../../components/ui/label';
import AuthLayout from '../../layouts/auth-layout';

interface ResetPasswordProps {
	token: string;
	email: string;
}

type ResetPasswordForm = {
	token: string;
	email: string;
	password: string;
	password_confirmation: string;
};

export default function ResetPassword({ token, email }: ResetPasswordProps) {
	const [form, setForm] = useState<ResetPasswordForm>({
		token: token,
		email: email,
		password: '',
		password_confirmation: '',
	});

	const [isLoading, setLoading] = useState<boolean>(false);
	const [error, setError] = useState(null);

	const submit: FormEventHandler = (e) => {
		e.preventDefault();
	};

	return (
		<AuthLayout title="Reset password" description="Please enter your new password below">
			<h1 title="Reset password" />

			<form onSubmit={submit}>
				<div className="grid gap-6">
					<div className="grid gap-2">
						<Label htmlFor="email">Email</Label>
						<Input
							id="email"
							type="email"
							name="email"
							autoComplete="email"
							value={form.email}
							className="mt-1 block w-full"
							readOnly
							onChange={(e) => setForm('email', e.target.value)}
						/>
						<InputError message={error.email} className="mt-2" />
					</div>

					<div className="grid gap-2">
						<Label htmlFor="password">Password</Label>
						<Input
							id="password"
							type="password"
							name="password"
							autoComplete="new-password"
							value={form.password}
							className="mt-1 block w-full"
							autoFocus
							onChange={(e) => setForm('password', e.target.value)}
							placeholder="Password"
						/>
						<InputError message={error.password} />
					</div>

					<div className="grid gap-2">
						<Label htmlFor="password_confirmation">Confirm password</Label>
						<Input
							id="password_confirmation"
							type="password"
							name="password_confirmation"
							autoComplete="new-password"
							value={form.password_confirmation}
							className="mt-1 block w-full"
							onChange={(e) => setForm('password_confirmation', e.target.value)}
							placeholder="Confirm password"
						/>
						<InputError message={error.password_confirmation} className="mt-2" />
					</div>

					<Button type="submit" className="mt-4 w-full" disabled={isLoading}>
						{isLoading && <LoaderCircle className="h-4 w-4 animate-spin" />}
						Reset password
					</Button>
				</div>
			</form>
		</AuthLayout>
	);
}
