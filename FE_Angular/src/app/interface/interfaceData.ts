export interface IUser {
  EmployeeId: string;
  Email: string;
  Name: string;
  Birthdate: Date;
  AvatarUrl: string;
  IsBan: boolean;
  Roles: IRole[];
}

export interface IRole {
  RoleId: string;
  RoleName: string;
}

export interface IEmployee {
  EmployeeId: string;
  Email: string;
  Name: string;
  Birthdate: Date;
  AvatarUrl: string;
  IsBan: boolean;
  Roles: IRole[];
}

export interface IEquipment {
  EquipmentId: string;
  Name: string;
  Description: string;
  EquipmentTypeId: string;
  EquipmentType: {
    EquipmentTypeId: string;
    TypeName: string;
  };
  ImageUrl: string;
  IsAvailable: boolean;
}

export interface IEquipmentType {
  EquipmentTypeId: string;
  TypeName: string;
}

export interface IEquipmentRequired {
  EquipmentHistoryId: string;
  IsAproved: boolean;
  BorrowedDate: Date;
  ReturnedDate: Date;
  Employee: IEmployee;
  Equipment: IEquipment;
}

export const localStorageToken: string = 'token';
