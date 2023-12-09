# Miku Model Replacement v1.3.0
### Replaces suits with Hatsune Miku. This is the example mod for ModelReplacementAPI

## Instructions
-
- Place in plugins folder next to boneMapMiku.json and ModelReplacementAPI.dll
- Set your preferred replacement suits in the config, or enable miku for all suits. By default the starting suit is replaced. 
- For more info see https://github.com/BunyaPineTree/LethalCompany_ModelReplacementAPI

## Known Issues
Cloth physics can be very laggy. It is recommended in larger lobbies that you lower the dynamic bone update rate and disable physics at range via the config. 


## Changelog
	- v1.3.0
		- Added config options to set model replacements for specific suit names, or all suits. 
		- The bug that prevented miku from disappearing after changing suits has been resolved, make sure to update to the altest ModelReplacementAPI version. 
	- v1.2.1
		- Fixed miku appearing in first person
	- v1.2.0
		- Added config options to improve dynamic bone performance. 
	- V1.1.0
		- Improved bone offsets, now actually only replaces the default suit, some script parameter changes
	- v1.0.0
		- Release