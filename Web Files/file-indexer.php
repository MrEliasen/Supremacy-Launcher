<?php
function indexDirectory( $dir, &$fileList )
{
	foreach ( scandir($dir) as $file )
	{
		if ( $file == '.' || $file == '..' )
		{
			continue;
		}

		if ( is_dir($dir . '/' . $file) )
		{
			indexDirectory($dir . '/' . $file, &$fileList );
		}
		else
		{
			$hash = sha1_file($dir . '/' . $file);
			$path = substr($dir, strpos($dir, 'downloads') + strlen('downloads/'));
			$path = explode('/', $path . '/' . $file);

			$data = "";
			$filename = "";
			foreach( $path as $k => $dirname )
			{
				$data .= '/' . $dirname;
				if($k == count($path) -1 )
				{
					$filename = $dirname;
				}
			}

			$fileList[] = array(
				'path' => $data,
				'name' => $filename,
				'sha1' => $hash
			);
		}
	}
}

$fileList = array();
indexDirectory(dirname(__FILE__) . '/downloads', $fileList);
$fileList = json_encode($fileList);

file_put_contents(dirname(__FILE__) . '/patchinfo.json', $fileList);

echo '<pre>' . $fileList;