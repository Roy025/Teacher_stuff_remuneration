import React, { useState } from 'react';
import '../Components/TeachersBill/TeachersBill.css';

export const Additions = ({ inputFields, defaultInput, setInputFields }) => {
	console.log(inputFields);

	const addInputField = (e) => {
		e.preventDefault();
		const clone = Object.assign({}, defaultInput);

		setInputFields([...inputFields, clone]);
	};

	return (
		<button
			className="btn-outline-success add"
			onClick={addInputField}
			type="button">
			<i className="fa-sharp fa-solid fa-plus"></i>
		</button>
	);
};
export const ThreeFields = ({
	inputFields,
	handleChange,
	removeInputFields,
}) => {
	return (
		<div className="Flex-row Row4 Common-info-flex-column">
			{inputFields.map((data, index) => {
				const { institute, department, member } = data;
				return (
					<div
						className="row"
						key={index}>
						<div className="form-group col">
							<label>Institute</label>
							<input
								type="text"
								name="institute"
								onChange={(evnt) => handleChange(evnt, index)}
								value={institute}
								className="form-control"
								id="institute"
								placeholder="Institute"
							/>
						</div>
						<div className="form-group col">
							<label>Department</label>
							<input
								type="text"
								name="department"
								onChange={(evnt) => handleChange(evnt, index)}
								value={department}
								className="form-control"
								id="department"
								placeholder="Department"
							/>
						</div>
						<div className="form-group col">
							<label>Member</label>
							<input
								type="text"
								name="member"
								onChange={(evnt) => handleChange(evnt, index)}
								value={member}
								className="form-control"
								id="member"
								placeholder="Member"
							/>
						</div>
						<div className="col text-center mt-3">
							{inputFields.length !== 1 ? (
								<button
									className="btn-outline-danger add"
									onClick={(evnt) => removeInputFields(evnt, index)}>
									x
								</button>
							) : (
								''
							)}
						</div>
					</div>
				);
			})}
		</div>
	);
};